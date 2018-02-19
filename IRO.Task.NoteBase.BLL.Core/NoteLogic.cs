using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.EF;
using System.Data.Entity;
using System.Linq;
using IRO.Task.NoteBase.DAL.Contracts;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class NoteLogic : INoteLogic
    {
        private readonly IMainConext _context;
        private readonly DbSet<Note> _dbSet;

        public NoteLogic(List<Book> bookList)
        {
            _context = new MainContext();
            _dbSet = _context.Notes;
            var __notesList = new List<Note>();
            foreach (var note in _dbSet)
            {
                if (bookList.Any((x) => x.Id == note.ParentBookId)) continue;
                __notesList.Add(note);
            }
            foreach (var note in __notesList)
                DeleteNote(note.Id);
            __notesList = null;
        }
        public bool AddNote(Note note)
        {
            if(_dbSet.Any(x => x.Id == note.Id))
            {
                return false;
            }
            _dbSet.Add(note);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteNote(long noteId)
        {
            var record = GetById(noteId);
            if (record == null)
                return false;
            var entry = _context.Entry(record);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }

        public List<Note> GetAll() => _dbSet.ToList();
        public List<Note> GetByBook(Book book) => _dbSet.Where(x => x.ParentBookId == book.Id).ToList();
        public Note GetById(long noteId) => _dbSet.FirstOrDefault(x => x.Id == noteId);
    }
}
