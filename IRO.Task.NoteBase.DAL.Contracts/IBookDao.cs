using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IBookDao
    {
        bool AddBook();
        bool GetBookByID();
        List<Book> GetBooksList();
        bool AddNote();
        bool GetNoteByID(uint noteID);
        List<Note> GetNotesList();

    }
}
