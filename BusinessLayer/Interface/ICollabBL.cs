namespace BusinessLayer.Interface
{
    using System.Collections.Generic;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    public interface ICollabBL
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabModel">null null.</param>
        /// <returns>AddCollaborator AddCollaborator.</returns>
        public CollabEntity AddCollaborator(CollabModel collabModel);

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns> null null.</returns>
        public CollabEntity RemoveCollab(long userId, long collabId);

        /// <summary>
        /// Gets the by note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>GetByNoteId GetByNoteId.</returns>
        public List<CollabEntity> GetByNoteId(long noteId, long userId);

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <returns>null null.</returns>
        public List<CollabEntity> GetAllCollab();
    }
}
