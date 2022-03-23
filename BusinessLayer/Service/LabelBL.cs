namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using BusinessLayer.Interface;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.ILabelBL" />
    public class LabelBL : ILabelBL
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL">The label rl.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.AddLabelName(labelName, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labeName">Name of the labe.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public LabelEntity UpdateLabel(string labeName, long noteId, long userId)
        {
            try
            {
                return this.labelRL.UpdateLabel(labeName, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the by labe identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public List<LabelEntity> GetByLabeId(long noteId)
        {
            try
            {
                return this.labelRL.GetByLabeId(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                return this.labelRL.RemoveLabel(labelId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>
        /// null null.
        /// </returns>
        public List<LabelEntity> GetAllLabels()
        {
            try
            {
                return this.labelRL.GetAllLabels();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
