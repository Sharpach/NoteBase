using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class MainContext : DbContext
    {
        public MainContext() : base("dbconnection")
        {
            //AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }

        public List<T> GetAll<T>() where T : class
        {
            var dbSet = Set<T>();
            return dbSet.ToList();
        }

        public void RemoveAll<T>() where T : class
        {
            var dbSet = Set<T>();
            foreach (var rec in dbSet.ToArray())
            {
                Entry(rec).State = EntityState.Deleted;
            }
        }
    }
}
