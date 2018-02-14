using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.EF;
using System.Data.Entity;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class BookLogic : IBookLogic
    {
        //TODO: intefraces EF
        private readonly MainContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookLogic()
        {
            _context = new MainContext();
            _dbSet = _context.Books;
        }

        public bool AddBook(Book book)
        {
            if (_dbSet.Any(x => x.Id == book.Id))
            {
                return false;
            }
            _dbSet.Add(book);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(int bookId)
        {
            Book record = GetById(bookId);
            if (record == null)
                return false;
            var entry = _context.Entry(record);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }

        public List<Book> GetByUser(User user)
        {
            return _dbSet.Where(x => x.OwnerId == user.Id)
                         .ToList();
        }

        public Book GetById(int bookId) => _dbSet.FirstOrDefault(x => x.Id == bookId);
    }
}
