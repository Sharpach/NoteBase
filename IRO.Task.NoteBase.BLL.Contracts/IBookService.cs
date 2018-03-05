using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IBookService
    {
        bool AddBook(Book book);
        bool ChangeBook(long bookId, string newName);
        bool DeleteBook(long bookId);
        List<Book> GetByUser(User user);
        Book GetById(long bookId);
        List<Book> GetAll();
        bool DeleteAllBooksByUser(User user);
    }
}
