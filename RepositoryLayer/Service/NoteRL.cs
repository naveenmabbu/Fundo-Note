using CommonLayer.Model;
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
    public class NoteRL: INoteRL
    {
        //instance of  FundooContext Class
        private readonly FundoContext fundoContext;
        private IConfiguration _config;

        //Constructor
        public NoteRL(FundoContext fundooContext, IConfiguration configuration)
        {
            this.fundoContext = fundooContext;
            this._config = configuration;

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
                    note.Colour = updateNote.Colour;
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
        public bool DeleteNote(long noteId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundoContext.Notes.Where(n => n.NotesId == noteId).FirstOrDefault();
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

        public NotesEntity getNote(long noteId)
        {
            try
            {
                // Fetch details with the given noteId.
                var note = this.fundoContext.Notes.Where(n => n.NotesId == noteId).FirstOrDefault();
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

    }
}
