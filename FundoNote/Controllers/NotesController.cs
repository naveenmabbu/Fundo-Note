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
    using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The noteBL
        /// </summary>
        private readonly INoteBL noteBL;

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="noteBL">The noteBL.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(INoteBL noteBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>null null.</returns>
        [HttpPost("Create")]
        public IActionResult CreateNote(Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.CreateNote(note, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = " note creating successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "note not created" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [HttpPut("Update")]
        public IActionResult UpdateNote(UpdateNote note, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.UpdateNote(note, noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes updated successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to update" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>null null.</returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteNote(long noteId, long userId)
        {
            try
            {
                var notes = this.noteBL.DeleteNote(noteId, userId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete the note" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = " Note is Deleted successfully ", data = notes });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [HttpGet("{noteId}/GetNote")]
        public IActionResult GetNotesByNotesId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.GetNotesByNotesId(noteId, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes are displayed", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to Display the notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>null null.</returns>
        [HttpGet("GetAllNotes")]
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                var result = this.noteBL.GetAllNotes();
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
        /// Gets all notes using redis cache.
        /// </summary>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "GetAllNotes";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await this.distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = (List<NotesEntity>) this.noteBL.GetAllNotes();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await this.distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }

            return this.Ok(NotesList);
        }

        /// <summary>
        /// Determines whether the specified note identifier is pinned.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [HttpPut("Pinned")]
        public IActionResult IsPinned(long noteId)
        {
            bool result = this.noteBL.IsPinned(noteId);

            try
            {
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [HttpPut("Trashed")]
        public IActionResult IsTrash(long noteId)
        {
            bool result = this.noteBL.IsTrash(noteId);

            try
            {
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>null null.</returns>
        [HttpPut("Archived")]
        public IActionResult IsArchive(long noteId)
        {
            bool result = this.noteBL.IsArchive(noteId);
            try
            {
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>null null.</returns>
        [Authorize]
        [HttpPost("ImageUpload")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                // Take id of  Logged In User
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.noteBL.UploadImage(noteId, userId, image);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Image Uploaded Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed ! Try Again " });
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
        /// <param name="noteId">The note identifier.</param>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>null null.</returns>
        [HttpPut("Colour")]
        public IActionResult ChangeColour(long noteId, ChangeColour notesModel)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            bool result = this.noteBL.ChangeColour(noteId, userId, notesModel);

            try
            {
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Color changed Successfully !!" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Color not changed !!" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}
