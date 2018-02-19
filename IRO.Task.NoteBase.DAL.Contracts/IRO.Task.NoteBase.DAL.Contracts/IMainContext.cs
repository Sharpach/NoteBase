using IRO.Task.NoteBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IMainConext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<User> Users { get; set; }
    }
}
