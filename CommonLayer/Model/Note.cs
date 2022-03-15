using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsTrash { get; set; }
        public bool Ispinned { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
