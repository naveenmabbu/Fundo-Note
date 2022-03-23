namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.INoteBL" />
    public class NoteBL : INoteBL
    {
        /// <summary>
        /// Gets or sets
        /// </summary>
        private readonly INoteRL noteRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteBL"/> class.
        /// </summary>
        /// <param name="noteRL">The note rl.</param>
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        /// <summary>
        /// CGets or sets
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel)
        {
            try
            {
                return this.noteRL.ChangeColour(noteId, userId, notesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public NotesEntity CreateNote(Note note, long userId)
        {
            try
            {
                return this.noteRL.CreateNote(note, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                return this.noteRL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>
        /// null null.
        /// </returns>
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                return this.noteRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId)
        {
            try
            {
                return this.noteRL.GetNotesByNotesId(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is archive.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is archive; otherwise, <c>false</c>.
        /// </returns>
        public bool IsArchive(long noteId)
        {
            try
            {
                return this.noteRL.IsArchive(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is pinned.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is pinned; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPinned(long noteId)
        {
            try
            {
                return this.noteRL.IsPinned(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified note identifier is trash; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTrash(long noteId)
        {
            try
            {
                return this.noteRL.IsTrash(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateNote">The update note.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId, long userId)
        {
            try
            {
                return this.noteRL.UpdateNote(updateNote, noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// null null.
        /// </returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                return this.noteRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
