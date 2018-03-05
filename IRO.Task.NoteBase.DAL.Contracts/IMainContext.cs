using IRO.Task.NoteBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IMainContext : IDisposable
    {
        DbSet<Book> Books { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
    }
}
