using System.Data.Entity;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class MainContext : DbContext
    {
        public MainContext() : base("dbconnection")
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            //Database.Delete();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
