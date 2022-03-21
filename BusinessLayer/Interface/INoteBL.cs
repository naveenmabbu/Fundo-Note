﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public bool DeleteNote(long notesId,long userId);
        public NotesEntity GetNoteId(long noteId, long userId);
        public List<NotesEntity> GetAllNotes();
        public bool IsPinned(long noteId);
        public bool IsTrash(long noteId);
        public bool IsArchive(long noteId);
        public NotesEntity UploadImage(long noteId, long userId, IFormFile image);
        public bool ChangeColour(long noteId, long userId, ChangeColour notesModel);
    }
}
