using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class NoteService : INoteService
    {
        IUnitOfWork Database { get; set; }

        public NoteService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public bool AddNote(Note note)
        {
            if (Database.Notes.GetAll().Any(x => x.Id == note.Id))
            {
                return false;
            }
            Database.Notes.Create(note);
            Database.Save();
            return true;
        }

        public bool ChangeNote(long noteId, string newText)
        {
            Note note = GetById(noteId);
            if (note == null) return false;           
            note.Text = newText;
            Database.Notes.Update(note);
            Database.Save();
            return true;
        }

        public bool DeleteNote(long noteId)
        {
            var note = GetById(noteId);
            if (note == null) return false;
            Database.Notes.Delete(noteId);
            Database.Save();
            return true;
        }

        public bool DeleteNotes(ICollection<Book> books)
        {
            foreach (var note in Database.Notes.GetAll())
            {
                if (books.Any((x) => x.Id == note.ParentBookId)) continue;
                Database.Notes.Delete(note.Id);
            }
            Database.Save();
            return true;
        }

        public List<Note> GetAll() => Database.Notes.GetAll().ToList();

        public List<Note> GetByBook(Book book) => Database.Notes.Find(x => x.ParentBookId == book.Id).ToList();

        public Note GetById(long noteId) => Database.Notes.Get(noteId);
    }
}
