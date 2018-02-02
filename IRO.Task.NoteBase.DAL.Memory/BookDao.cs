using System;
using System.Collections.Generic;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Memory
{
    class BookDao : IBookDao
    {
        public bool AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public bool AddNote(Note note)
        {
            throw new NotImplementedException();
        }

        public bool GetBookByID(uint bookID)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetBooksList()
        {
            throw new NotImplementedException();
        }

        public bool GetNoteByID(uint noteID)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetNotesList()
        {
            throw new NotImplementedException();
        }
    }
}
