namespace RepositoryLayer.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ok ok
    /// </summary>
    public class CollabEntity
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <value>
        /// ok ok
        /// </value>
        public string CollabEmail { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserEntity user { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        [ForeignKey("notes")]
        public long NotesId { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public NotesEntity notes { get; set; }
    }
}
