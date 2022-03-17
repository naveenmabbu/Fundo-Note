using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Context;
using System;
using CommonLayer.Model;
using System.Linq;
using System.Collections.Generic;

namespace RepositoryLayer.Service
{
    public class CollabRL: ICollabRL
    {
        private readonly FundoContext fundoContext;

        public CollabRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                CollabEntity collaboration = new CollabEntity();
                var user = fundoContext.User.Where(e => e.Email == collabModel.CollabEmail).FirstOrDefault();

                var notes = fundoContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collabModel.NotesId;
                    collaboration.CollabEmail = collabModel.CollabEmail;
                    collaboration.Id = collabModel.Id;
                    fundoContext.Collab.Add(collaboration);
                    var result = fundoContext.SaveChanges();
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
        public CollabEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                var data = this.fundoContext.Collab.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
                if (data != null)
                {
                    this.fundoContext.Collab.Remove(data);
                    this.fundoContext.SaveChanges();
                    return data;
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
        public List<CollabEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                var data = this.fundoContext.Collab.Where(c => c.NotesId == noteId && c.Id == userId).ToList();
                if (data != null)
                {
                    return data;
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
