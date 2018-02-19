using System.Data.Entity;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.Contracts;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class MainContext : DbContext, IMainContext
    {
        public MainContext() : base("dbconnection")
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            //Database.Delete();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        void IMainContext.SaveChanges() => base.SaveChanges();
        System.Data.Entity.Infrastructure.DbEntityEntry IMainContext.Entry(object entity) => base.Entry(entity);
    }
}
