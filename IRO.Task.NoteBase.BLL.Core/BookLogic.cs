using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.EF;
using System.Data.Entity;
using System.Linq;
using IRO.Task.NoteBase.DAL.Contracts;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class BookLogic : IBookLogic
    {
        private readonly IMainContext _context;
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

        public bool ChangeBook(long bookId, string newName)
        {
            Book book = GetById(bookId);
            var entry = _context.Entry(book);
            book.Name = newName;
            entry.State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBook(long bookId)
        {
            var record = GetById(bookId);
            if (record == null)
                return false;

            var entry = _context.Entry(record);
            entry.State = EntityState.Deleted;
            if(save)
                _context.SaveChanges();
            return true;
        }

        public List<Book> GetByUser(User user)
        {
            return _dbSet.Where(x => x.OwnerId == user.Id)
                         .ToList();
        }

        public Book GetById(long bookId) => _dbSet.FirstOrDefault(x => x.Id == bookId);

        public List<Book> GetAll() => _dbSet.ToList();

        public bool DeleteAllBooksByUser(User user)
        {
            var books = _dbSet.Where(x => x.OwnerId == user.Id);
            foreach (var book in books)
            {
                DeleteBook(book.Id, false);
            }
            SaveChanges();
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
