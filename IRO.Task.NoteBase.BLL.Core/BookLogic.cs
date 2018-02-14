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
            _dbSet.Add(book);
            _context.SaveChanges();
            return true;
        }

        public List<Book> GetByUser(User user)
        {
            return _dbSet.Where(x => x.Owner.Id == user.Id)
                         .ToList();
        }

        public Book GetById(int bookId) => _dbSet.FirstOrDefault(x => x.Id == bookId);
    }
}
