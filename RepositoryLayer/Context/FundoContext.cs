namespace RepositoryLayer.Context
{
    using Microsoft.EntityFrameworkCore;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class FundoContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FundoContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public FundoContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public DbSet<UserEntity> User { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public DbSet<NotesEntity> Notes { get; set; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <value>
        /// ok ok
        /// </value>
        public DbSet<CollabEntity> Collab { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public DbSet<LabelEntity> Label { get; set; }
    }
}
