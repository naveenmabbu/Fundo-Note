namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// ok ok
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.INoteRL" />
    public class NoteRL : INoteRL
    {
        /// <summary>
        /// The fundo context
        /// </summary>
        private readonly FundoContext fundoContext;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _config;

        ////Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundoo context.</param>
        /// <param name="_config">The configuration.</param>
        public NoteRL(FundoContext fundooContext, IConfiguration _config)
        {
            this.fundoContext = fundooContext;
            this._config = _config;
        }

        ////Method to Notes Details.
        
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>create create</returns>
        public NotesEntity CreateNote(Note note, long userId)
        {
            try
            {
                NotesEntity newNotes = new NotesEntity();
                newNotes.Title = note.Title;
                newNotes.Description = note.Description;
                newNotes.Colour = note.Colour;
                newNotes.Image = note.Image;
                newNotes.IsArchive = note.IsArchive;
                newNotes.IsTrash = note.IsTrash;
                newNotes.Ispinned = note.Ispinned;
                newNotes.CreatedAt = note.CreatedAt;
                newNotes.ModifiedAt = note.ModifiedAt;
                newNotes.Id = userId;
                this.fundoContext.Notes.Add(newNotes);
                int result = this.fundoContext.SaveChanges();
                if (result > 0)
                {
                    return newNotes;
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

        ////Method to UpdateNote Details.
        
        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="updateNote">The update note.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>update update</returns>
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId, long userId)
        {
            try
            {
                var notes = this.fundoContext.Notes.Where(update => update.NotesId == noteId && update.Id == userId).FirstOrDefault();
                if (notes != null)
                {
                    notes.Title = updateNote.Title;
                    notes.Description = updateNote.Description;
                    notes.Image = updateNote.Image;
                    notes.ModifiedAt = updateNote.ModifiedAt;
                    this.fundoContext.Notes.Update(notes);
                    this.fundoContext.SaveChanges();
                    return notes;
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

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>delete delete</returns>
        public bool DeleteNote(long noteId, long userId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundoContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (note != null)
                {
                    // Remove Note details from database
                    this.fundoContext.Notes.Remove(note);

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

        /// <summary>
        /// Gets the notes by notes identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>get get</returns>
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId)
        {
            try
            {
                var notesResult = this.fundoContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).ToList();
                if (notesResult != null)
                {
                    return notesResult;
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

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>GetAllNotes GetAllNotes.</returns>
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                // Fetch All the details from Notes Table
                var notes = this.fundoContext.Notes.ToList();
                if (notes != null)
                {
                    return notes;
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

        ////Method to IsPinned Details.
        
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
                var notes = this.fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

                if (notes != null)
                {
                    if (notes.Ispinned == true)
                    {
                        notes.Ispinned = false;
                    }
                    else if (notes.Ispinned == false)
                    {
                        notes.Ispinned = true;
                    }

                    notes.ModifiedAt = DateTime.Now;
                }

                int changes = this.fundoContext.SaveChanges();

                if (changes > 0)
                {
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

        ////Method to IsTrash Details. 
        
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
                var notes = this.fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

                if (notes != null)
                {
                    if (notes.IsTrash == true)
                    {
                        notes.IsTrash = false;
                    }
                    else if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                    }

                    notes.ModifiedAt = DateTime.Now;
                }

                int changes = this.fundoContext.SaveChanges();

                if (changes > 0)
                {
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

        ////Method to IsArchive Details.
        
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
                var notes = this.fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

                if (notes != null)
                {
                    if (notes.IsArchive == true)
                    {
                        notes.IsArchive = false;
                    }
                    else if (notes.IsArchive == false)
                    {
                        notes.IsArchive = true;
                    }

                    notes.ModifiedAt = DateTime.Now;
                }

                int changes = this.fundoContext.SaveChanges();

                if (changes > 0)
                {
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
        
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>upload upload</returns>
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.fundoContext.Notes.FirstOrDefault(n => n.NotesId == noteId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(_config["Cloudinary:CloudName"], _config["Cloudinary:ApiKey"], _config["Cloudinary:ApiSecret"]);
                    Cloudinary cloud = new Cloudinary(acc);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    this.fundoContext.Notes.Update(note);
                    int upload = this.fundoContext.SaveChanges();
                    if (upload > 0)
                    {
                        return note;
                    }
                    else
                    {
                        return null;
                    }
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

        ////Method to ChangeColor Details.

        /// <summary>
        /// Gets or sets
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>colour colour</returns>
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel)
        {
            try
            {
                var result = this.fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.Colour = notesModel.Colour;
                    result.ModifiedAt = DateTime.Now;
                }

                int changes = this.fundoContext.SaveChanges();

                if (changes > 0)
                {
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
    }
}
