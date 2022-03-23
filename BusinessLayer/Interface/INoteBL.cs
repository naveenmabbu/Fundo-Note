namespace BusinessLayer.Interface
{
    using System.Collections.Generic;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;

    /// <summary>
    /// ok ok
    /// </summary>
    public interface INoteBL
    {
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>null null.</returns>
        public NotesEntity CreateNote(Note note, long userId);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateNote">The update note.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>null null.</returns>
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId, long userId);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>null null.</returns>
        public bool DeleteNote(long notesId, long userId);

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>null null.</returns>
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>null null.</returns>
        public List<NotesEntity> GetAllNotes();

        /// <summary>
        /// Determines whether the specified note identifier is pinned.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is pinned; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPinned(long noteId);

        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is trash; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTrash(long noteId);

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is archive; otherwise, <c>false</c>.
        /// </returns>
        public bool IsArchive(long noteId);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>null null.</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);

        /// <summary>
        /// CGets or sets
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>null null.</returns>
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel);
    }
}
