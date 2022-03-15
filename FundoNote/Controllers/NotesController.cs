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
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NotesController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }
        [Authorize]
        [HttpPost("CreateNote")]
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
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateNote(UpdateNote note, long userId)
        {
            try
            {
                var result = noteBL.UpdateNote(note, userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Notes updated successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Update failed" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, Message = e.Message });
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                var notes = this.noteBL.DeleteNote(noteId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete note" });
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
        [Authorize]
        [HttpGet("GetNoteByUserId")]
        public IActionResult GetNotesByUserId(long userId)
        {
            try
            {
                var notes = this.noteBL.GetNotesByUserId(userId);
                if (notes != null)
                {
                    return this.Ok(new { Success = true, message = "Notes are displayed", data = notes });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "failed to Display the notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("GetNote")]
        public NotesEntity GetNote(long noteId)
        {
            try
            {
                var result = this.noteBL.getNote(noteId);
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
    }
}
