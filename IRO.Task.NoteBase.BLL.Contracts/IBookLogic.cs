using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    interface IBookLogic
    {
        bool AddBook(Book book);
        List<Book> GetAll();
        Book GetBookByID(uint bookID);
    }
}