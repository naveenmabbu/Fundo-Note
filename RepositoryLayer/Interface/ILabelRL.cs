namespace RepositoryLayer.Interface
{
    using System.Collections.Generic;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    public interface ILabelRL
    {
        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>add add</returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labeName">Name of the labe.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>update update</returns>
        public LabelEntity UpdateLabel(string labeName, long noteId, long userId);

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>remove remove</returns>
        public bool RemoveLabel(long labelId, long userId);

        /// <summary>
        /// Gets the by labe identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>get get</returns>
        public List<LabelEntity> GetByLabeId(long noteId);

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <returns>getall getall</returns>
        public List<LabelEntity> GetAllLabels();
    }
}
