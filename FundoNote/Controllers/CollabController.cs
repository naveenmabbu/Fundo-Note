using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundoNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                CollabModel collaboratorModel = new CollabModel();
                collaboratorModel.Id = userId;
                collaboratorModel.NotesId = noteId;
                collaboratorModel.CollabEmail = email;
                var result = this.collabBL.AddCollaborator(collaboratorModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Added  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Add Failed ! Try Again" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
