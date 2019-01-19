using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class MainContext : DbContext, IMainContext
    {        
        private string ConnectionString { get; set; }

        public MainContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        //public MainContext(DbContextOptions<MainContext> options) : base(options)
        //{
        //}

        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        void IMainContext.SaveChanges() => base.SaveChanges();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
