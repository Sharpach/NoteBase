using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.Memory;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class BookLogic : IBookLogic
    {
        private readonly BookDao _bookDao;

        public BookLogic()
        {
            _bookDao = new BookDao();
        }
        public bool AddBook(Book book) => _bookDao.AddBook(book);

        public List<Book> GetAll() => _bookDao.GetAll();
        public List<Book> GetByUser(User user) => _bookDao.GetByUser(user);
        public Book GetById(uint bookId) => _bookDao.GetById(bookId);
    }
}
