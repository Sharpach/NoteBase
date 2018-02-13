using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface INoteDao
    {
        bool AddNote(Note note);
        Note GetById(int noteId);
        List<Note> GetByBook(Book book);
        List<Note> GetAll();
    }
}
