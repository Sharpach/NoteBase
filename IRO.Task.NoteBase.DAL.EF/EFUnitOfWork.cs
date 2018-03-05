using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private IMainContext db;

        private BookRepository bookRepository;
        private UserRepository userRepository;
        private NoteRepository noteRepository;

        public EFUnitOfWork(IMainContext context)
        {
            db = context;
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Book> Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IRepository<Note> Notes
        {
            get
            {
                if (noteRepository == null)
                    noteRepository = new NoteRepository(db);
                return noteRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region Disposed pattern

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
