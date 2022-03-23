namespace FundoNote.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// Gets or Sets
        /// </summary>
        private readonly ILabelBL labelBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelBL">The labelBL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddLabel(string labelName, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                var label = this.labelBL.AddLabelName(labelName, noteId, userId);
                if (label != null)
                {
                    return this.Ok(new { success = true, message = "Label Added Successfully", data = label });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Label adding UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateLabelName(string labelName, long noteId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var notes = this.labelBL.UpdateLabel(labelName, noteId, userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = " Label Name Updated  successfully ", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Failed to update" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelId)
        {
            try
            {
                // Take id of  Logged In User
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.labelBL.RemoveLabel(labelId, userId))
                {
                    return this.Ok(new { Success = true, message = " Label Removed  successfully " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Label Remove Failed " });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the by label identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpGet("{noteId}/Get")]
        public List<LabelEntity> GetByLabelId(long noteId)
        {
            try
            {
                var result = this.labelBL.GetByLabeId(noteId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>null null.</returns>
        [HttpGet("GetAll")]
        public List<LabelEntity> GetAllLabels()
        {
            try
            {
                var result = this.labelBL.GetAllLabels();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets or Sets.
        /// </summary>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetByLabelsUsingRedisCache()
        {
            var cacheKey = " GetByLabels";
            string serializedLabelsList;
            var LabelsList = new List<LabelEntity>();
            var redisLabelsList = await this.distributedCache.GetAsync(cacheKey);
            if (redisLabelsList != null)
            {
                serializedLabelsList = Encoding.UTF8.GetString(redisLabelsList);
                LabelsList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelsList);
            }
            else
            {
                LabelsList = (List<LabelEntity>)this.labelBL.GetAllLabels();
                serializedLabelsList = JsonConvert.SerializeObject(LabelsList);
                redisLabelsList = Encoding.UTF8.GetBytes(serializedLabelsList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisLabelsList, options);
            }

            return this.Ok(LabelsList);
        }
    }
}
