using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IBookDao
    {
        bool AddBook(Book book);
        Book GetById(int bookId);
        List<Book> GetByUser(User user);
        List<Book> GetAll();
    }
}
