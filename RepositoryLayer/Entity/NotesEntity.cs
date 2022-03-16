﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string Colour { get; set; }
        public string Image { get; set; }
        public bool IsTrash{ get; set; }
        public bool IsArchive { get; set; }
        public bool Ispinned { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }
        public UserEntity user { get; set; }

    }
}