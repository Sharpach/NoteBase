using System.Data.Entity;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.EF
{
    public interface IMainContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }
    }
}