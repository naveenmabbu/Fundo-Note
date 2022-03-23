namespace CommonLayer.Model
{
    using System;

    /// <summary>
    /// ok ok
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets .
        /// </summary>
        /// <value>
        /// The reminder.
        /// </value>
        public DateTime Reminder { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <value>
        /// The colour.
        /// </value>
        public string Colour { get; set; }

        /// <summary>
        /// Gets or sets 
        /// </summary>
        /// <value>
        /// ok ok.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is archive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is archive; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trash.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is trash; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrash { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Note"/> is ispinned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if ispinned; otherwise, <c>false</c>.
        /// </value>
        public bool Ispinned { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the modified at.
        /// </summary>
        /// <value>
        /// The modified at.
        /// </value>
        public DateTime? ModifiedAt { get; set; }
    }
}
