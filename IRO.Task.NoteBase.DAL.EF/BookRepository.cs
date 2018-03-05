using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class BookRepository : IRepository<Book>
    {
        private IMainContext db;

        public BookRepository(IMainContext context)
        {
            db = context;
        }

        public void Create(Book item)
        {
            db.Books.Add(item);
        }

        public void Delete(long id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
        }

        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            return db.Books.Where(predicate).ToList();
        }

        public Book Get(long id)
        {
            return db.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public void Update(Book item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Books.Update(item);
        }
    }
}
