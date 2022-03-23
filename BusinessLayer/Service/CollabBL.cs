namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.ICollabBL" />
    public class CollabBL : ICollabBL
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly ICollabRL collabRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabBL"/> class.
        /// </summary>
        /// <param name="collabRL">The collab rl.</param>
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">null null.</param>
        /// <returns>
        /// AddCollaborator AddCollaborator.
        /// </returns>
        public CollabEntity AddCollaborator(CollabModel collabModel)
        {
            try
            {
                return this.collabRL.AddCollaborator(collabModel);
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
        /// <returns>
        /// GetByNoteId GetByNoteId.
        /// </returns>
        public List<CollabEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                return this.collabRL.GetByNoteId(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public CollabEntity RemoveCollab(long userId, long collabId)
        {
            try
            {
                return this.collabRL.RemoveCollab(userId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <returns>
        /// null null.
        /// </returns>
        public List<CollabEntity> GetAllCollab()
        {
            try
            {
                return this.collabRL.GetAllCollab();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
