namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.ILabelRL" />
    public class LabelRL : ILabelRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundoContext fundoContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRL"/> class.
        /// </summary>
        /// <param name="fundoContext">The fundo context.</param>
        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        /// <summary>
        /// Adds the name of the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>add add</returns>
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity
                {
                    LabelName = labelName,
                    Id = userId,
                    NotesId = noteId
                };
                this.fundoContext.Label.Add(labelEntity);
                int result = this.fundoContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
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
        /// Updates the label.
        /// </summary>
        /// <param name="labeName">Name of the labe.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>update update</returns>
        public LabelEntity UpdateLabel(string labeName, long noteId, long userId)
        {
            try
            {
                var label = this.fundoContext.Label.FirstOrDefault(a => a.NotesId == noteId && a.Id == userId);
                if (label != null)
                {
                    label.LabelName = labeName;
                    this.fundoContext.Label.Update(label);
                    this.fundoContext.SaveChanges();
                    return label;
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
        /// Removes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>remove remove</returns>
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                var labelDetails = this.fundoContext.Label.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    this.fundoContext.Label.Remove(labelDetails);

                    // Save Changes Made in the database
                    this.fundoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// <returns>get get</returns>
        public List<LabelEntity> GetByLabeId(long noteId)
        {
            try
            {
                // Fetch All the details with the given noteid.
                var data = this.fundoContext.Label.Where(d => d.NotesId == noteId).ToList();
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
        /// Gets all labels.
        /// </summary>
        /// <returns>getall getall</returns>
        public List<LabelEntity> GetAllLabels()
        {
            try
            {
                // Fetch All the details from Labels Table
                var labels = this.fundoContext.Label.ToList();
                if (labels != null)
                {
                    return labels;
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
