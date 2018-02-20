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

        public BookLogic(ICollection<User> userList)
        {
            _context = new MainContext();
            _dbSet = _context.Books;
            DeleteAllBooks(userList);
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

        private void DeleteAllBooks(ICollection<User> userList)
        {
            foreach (var book in _dbSet)
            {
                if (userList.Any(x => x.Id == book.OwnerId)) continue;
                DeleteBook(book.Id);
            }
        }
    }
}
