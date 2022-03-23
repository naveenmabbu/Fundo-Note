namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommonLayer.Model;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ICollabRL" />
    public class CollabRL : ICollabRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundoContext fundoContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRL"/> class.
        /// </summary>
        /// <param name="fundoContext">The fundo context.</param>
        public CollabRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">The collab model.</param>
        /// <returns>add add</returns>
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                CollabEntity collaboration = new CollabEntity();
                var user = this.fundoContext.User.Where(e => e.Email == collabModel.CollabEmail).FirstOrDefault();

                var notes = this.fundoContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
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

        /// <summary>
        /// Gets or sets.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns>remove remove</returns>
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

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>get get</returns>
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

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <returns>getall getall</returns>
        public List<CollabEntity> GetAllCollab()
        {
            try
            {
                // Fetch All the details from Collab Table
                var collabs = this.fundoContext.Collab.ToList();
                if (collabs != null)
                {
                    return collabs;
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
