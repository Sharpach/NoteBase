using System;
using System.Collections.Generic;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Memory
{
    public class BookDao : IBookDao
    {
        private List<Book> Memory;
        public BookDao()
        {
            Memory = new List<Book>();
        }
        public bool AddBook(Book book)
        {
            if (Memory.Count == 0)
                book.Id = 0;
            else
                book.Id = Memory[Memory.Count - 1].Id + 1;
            Memory.Add(book);
            return true;
        }

        public Book GetByID(uint bookId) => Memory.Find(x => x.Id == bookId);
        public List<Book> GetByUser(User user) => Memory.FindAll(x => x.Owner == user);
        public List<Book> GetAll() => Memory;
    }
}
