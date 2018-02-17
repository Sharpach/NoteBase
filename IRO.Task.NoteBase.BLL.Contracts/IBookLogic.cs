using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IBookLogic
    {
        bool AddBook(Book book);
        bool DeleteBook(long bookId);
        List<Book> GetByUser(User user);
        Book GetById(long bookId);
        List<Book> GetAll();
    }
}