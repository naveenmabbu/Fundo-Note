using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;

        }

        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel)
        {
            try
            {
                return noteRL.ChangeColour(noteId, userId,notesModel);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public NotesEntity CreateNote(Note note, long userId)
        {
            try
            {
                return noteRL.CreateNote(note, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public bool DeleteNote(long noteId,long userId)
        {
            try
            {
                return noteRL.DeleteNote(noteId, userId);
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
                return noteRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId)
        {
            try
            {
                return noteRL.GetNotesByNotesId(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsArchive(long noteId)
        {
            try
            {
                return noteRL.IsArchive(noteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsPinned(long noteId)
        {
            try
            {
                return noteRL.IsPinned(noteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsTrash(long noteId)
        {
            try
            {
                return noteRL.IsTrash(noteId);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId, long userId)
        {
            try
            {
                return noteRL.UpdateNote(updateNote, noteId,userId);

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
                return this.noteRL.UploadImage(noteId, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
