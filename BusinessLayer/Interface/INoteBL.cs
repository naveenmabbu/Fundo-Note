using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NotesEntity CreateNote(Note note, long userId);
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId);
        public bool DeleteNote(long noteId);
        public NotesEntity getNote(long noteId);
        public List<NotesEntity> GetNotesByUserId(long userId);
        public List<NotesEntity> GetAllNotes();
    }
}
