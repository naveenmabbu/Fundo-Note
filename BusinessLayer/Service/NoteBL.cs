using BusinessLayer.Interface;
using CommonLayer.Model;
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

        public bool DeleteNote(long noteId)
        {
            try
            {
                return noteRL.DeleteNote(noteId);
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

        public NotesEntity getNote(long noteId)
        {
            try
            {
                return noteRL.getNote(noteId);
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
                return noteRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId)
        {
            try
            {
                return noteRL.UpdateNote(updateNote, noteId);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
