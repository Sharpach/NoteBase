using IRO.Task.NoteBase.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Book GetByID(uint bookID) => _bookDao.GetByID(bookID);
    }
}
