using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        //instance of  FundooContext Class
        private readonly FundoContext fundoContext;

        private readonly IConfiguration _config;

        
        //Constructor
        public NoteRL(FundoContext fundooContext, IConfiguration _config)
        {
            this.fundoContext = fundooContext;
            this._config = _config;

        }
        //Method to Notes Details.
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
                fundoContext.Notes.Add(newNotes);
                int result = fundoContext.SaveChanges();
                if (result > 0)
                    return newNotes;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to UpdateNote Details.
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId)
        {
            try
            {
                var note = fundoContext.Notes.Where(update => update.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Title = updateNote.Title;
                    note.Description = updateNote.Description;
                    note.Image = updateNote.Image;
                    note.ModifiedAt = updateNote.ModifiedAt;
                    note.Id = noteId;
                    fundoContext.Notes.Update(note);
                    int result = fundoContext.SaveChanges();
                    return note;
                }

                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
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

        public NotesEntity GetNoteId(long noteId, long userId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundoContext.Notes.Where(n => n.NotesId == noteId && n.Id == userId).FirstOrDefault();
                if (note != null)
                {

                    return note;
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

        public List<NotesEntity> GetNotesByUserId(long userId)
        {
            try
            {
                //fetch all the notes with user id
                var notes = this.fundoContext.Notes.Where(n => n.Id == userId).ToList();
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
        //Method to IsPinned Details.
        public bool IsPinned(long noteId)
        {
            try
            {
                var notes = fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

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
                int changes = fundoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Method to IsTrash Details.
        public bool IsTrash(long noteId)
        {
            try
            {
                var notes = fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

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
                int changes = fundoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to IsArchive Details.
        public bool IsArchive(long noteId)
        {
            try
            {
                var notes = fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId);

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
                int changes = fundoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }
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
        //Method to ChangeColor Details.
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel)
        {
            try
            {
                var result = fundoContext.Notes.FirstOrDefault(e => e.NotesId == noteId && e.Id == userId);

                if (result != null)
                {
                    result.Colour = notesModel.Colour;
                    result.ModifiedAt = DateTime.Now;
                }
                int changes = fundoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
