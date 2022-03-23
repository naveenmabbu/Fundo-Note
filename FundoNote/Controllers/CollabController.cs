namespace FundoNote.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
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
    public class CollabController : ControllerBase
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly ICollabBL collabBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabController"/> class.
        /// </summary>
        /// <param name="collabBL">The collabBL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Gets or sets.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                CollabModel collaborator = new CollabModel();
                collaborator.Id = userId;
                collaborator.NotesId = noteId;
                collaborator.CollabEmail = email;
                var result = this.collabBL.AddCollaborator(collaborator);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collaborator Added  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collaborator Add Failed ! Try Again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveCollab(long collabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.RemoveCollab(userId, collabId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpGet("{noteId}/Get")]
        public List<CollabEntity> GetByNoteId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.GetByNoteId(noteId, userId);
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
        /// Gets or sets
        /// </summary>
        /// <returns>null null.</returns>
        [HttpGet("GetAll")]
        public List<CollabEntity> GetAllCollab()
        {
            try
            {
                var result = this.collabBL.GetAllCollab();
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
        /// Gets or sets
        /// </summary>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetByCollabUsingRedisCache()
        {
            var cacheKey = " GetByCollab";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await this.distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = (List<CollabEntity>)this.collabBL.GetAllCollab();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }

            return this.Ok(CollabList);
        }
    }
}
