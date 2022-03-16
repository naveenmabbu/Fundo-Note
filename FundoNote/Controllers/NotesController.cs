using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NotesController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }
        [HttpPost("Create")]
        public IActionResult CreateNote(Note note)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = noteBL.CreateNote(note, userId);
                if (result != null)
                    return this.Ok(new { success = true, message = " note creating successful", data = result });
                else
                    return this.BadRequest(new { success = false, message = "note not created" });

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateNote(UpdateNote note, long noteId)
        {
            try
            {
                var result = noteBL.UpdateNote(note, noteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes updated successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "failed to update" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
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
        [HttpGet("{userid}/Get")]
        public IActionResult GetNotesByUserId(long userId)
        {
            try
            {
                var notes = this.noteBL.GetNotesByUserId(userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = "notes displayed", data = notes });
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
        [HttpGet("{id}/Get")]
        public NotesEntity GetNoteId(long noteId, long userId)
        {
            try
            {
                var result = this.noteBL.GetNoteId(noteId, userId);
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
        [HttpPut("Pinned")]
        public IActionResult IsPinned(long noteId)
        {
            bool result = noteBL.IsPinned(noteId);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //IsTrashed 

        [HttpPut("Trashed")]
        public IActionResult IsTrash(long noteId)
        {
            bool result = noteBL.IsTrash(noteId);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Archived")]
        public IActionResult IsArchive(long noteId)
        {
            bool result = noteBL.IsArchive(noteId);


            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
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
        //Change colour

        [HttpPut("Colour")]
        public IActionResult ChangeColour(long noteId, ChangeColour notesModel)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            bool result = noteBL.ChangeColour(noteId, userId, notesModel);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Color changed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Color not changed !!" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Success = false, message = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}
