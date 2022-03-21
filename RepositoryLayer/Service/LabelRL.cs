using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundoContext fundoContext;

        public LabelRL(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
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
