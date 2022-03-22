using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NotesEntity CreateNote(Note note, long userId);
        public NotesEntity UpdateNote(UpdateNote updateNote, long noteId, long userId);
        public bool DeleteNote(long notesId,long userId);
        public List<NotesEntity> GetNotesByNotesId(long noteId, long userId);
        public List<NotesEntity> GetAllNotes();
        public bool IsPinned(long noteId);
        public bool IsTrash(long noteId);
        public bool IsArchive(long noteId);
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel);


    }
}
