using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface INoteLogic
    {
        bool AddNote(Note note);
        bool DeleteNote(long noteId);
        List<Note> GetAll();
        List<Note> GetByBook(Book book);
        Note GetById(long noteId);
        bool DeleteNotes(ICollection<Book> books);
    }
}