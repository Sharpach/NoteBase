using System.Data.Entity;
using IRO.Task.NoteBase.Entities;
using System;
using IRO.Task.NoteBase.DAL.Contracts;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class MainContext : DbContext, IMainConext
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
