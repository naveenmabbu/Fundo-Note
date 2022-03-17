using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabelName(string labelName, long noteId, long userId);
        public LabelEntity UpdateLabel(string labeName, long noteId, long userId);
        public bool RemoveLabel(long labelId, long userId);
        public List<LabelEntity> GetByLabeId(long noteId);
    }
}
