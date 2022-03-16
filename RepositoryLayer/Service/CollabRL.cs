using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Context;
using System;
using CommonLayer.Model;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class CollabRL: ICollabRL
    {
        private readonly FundoContext fundooContext;

        public CollabRL(FundoContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                CollabEntity collaboration = new CollabEntity();
                var user = fundooContext.User.Where(e => e.Email == collabModel.CollabEmail).FirstOrDefault();

                var notes = fundooContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collabModel.NotesId;
                    collaboration.CollabEmail = collabModel.CollabEmail;
                    collaboration.Id = collabModel.Id;
                    fundooContext.Collab.Add(collaboration);
                    var result = fundooContext.SaveChanges();
                    return collaboration;
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
