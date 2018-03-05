using System;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.Contracts;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class BookService : IBookService
    {
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public Book GetById(long id)
        {         
            var book = Database.Books.Get(id);
            if (book == null) throw new Exception("Книга не найдена");
            return book;
        }

        public List<Book> GetAll()
        {
            return Database.Books.GetAll().ToList();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public bool AddBook(Book book)
        {
            if (Database.Books.GetAll().ToList().Any(x => x.Id == book.Id))
            {
                return false;
            }
            Database.Books.Create(book);
            Database.Save();
            return true;
        }

        public bool ChangeBook(long bookId, string newName)
        {
            Book book = GetById(bookId);
            if (book == null) return false;
            book.Name = newName;
            Database.Books.Update(book);
            Database.Save();
            return true;
        }

        public bool DeleteBook(long bookId)
        {
            var book = GetById(bookId);
            if (book == null) return false;
            Database.Books.Delete(bookId);
            Database.Save();
            return true;
        }

        public List<Book> GetByUser(User user)
        {
            return Database.Books.Find(x => x.OwnerId == user.Id).ToList();
        }

        public bool DeleteAllBooksByUser(User user)
        {
            var books = GetByUser(user);
            foreach (var book in books)
            {
                Database.Books.Delete(book.Id);
            }
            Database.Save();
            return true;
        }
    }
}
