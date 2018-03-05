using IRO.Task.NoteBase.Entities;
using System;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Book> Books { get; }
        IRepository<Note> Notes { get; }
        void Save();
    }
}
