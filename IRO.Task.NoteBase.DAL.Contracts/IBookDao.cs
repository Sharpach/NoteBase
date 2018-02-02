using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IBookDao
    {
        bool AddBook(Book book);
        bool GetBookByID(uint bookID);
        List<Book> GetBooksList();
        bool AddNote(Note note);
        bool GetNoteByID(uint noteID);
        List<Note> GetNotesList();
    }
}
