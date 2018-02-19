using IRO.Task.NoteBase.Entities;
using System.Data.Entity;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IMainContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
    }
}
