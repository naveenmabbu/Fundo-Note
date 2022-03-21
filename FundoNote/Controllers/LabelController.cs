using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
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
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetByLabelsUsingRedisCache()
        {
            var cacheKey = " GetByLabels";
            string serializedLabelsList;
            var LabelsList = new List<LabelEntity>();
            var redisLabelsList = await distributedCache.GetAsync(cacheKey);
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
                await distributedCache.SetAsync(cacheKey, redisLabelsList, options);
            }
            return Ok(LabelsList);
        }
    }
}
