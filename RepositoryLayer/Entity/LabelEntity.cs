namespace RepositoryLayer.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ok ok
    /// </summary>
    public class LabelEntity
    {
        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <value>
        /// ok ok
        /// </value>
        public string LabelName { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("notes")]
        public long NotesId { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        /// <value>
        /// ok ok
        /// </value>
        public NotesEntity notes { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [ForeignKey("user")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>
        /// ok ok
        /// </value>
        public UserEntity user { get; set; }
    }
}
