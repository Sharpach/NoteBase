using IRO.Task.NoteBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using IRO.Task.NoteBase.DAL.Contracts;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class NoteRepository : IRepository<Note>
    {
        private IMainContext db;

        public NoteRepository(IMainContext context)
        {
            db = context;
        }

        public void Create(Note item)
        {
            db.Notes.Add(item);
        }

        public void Delete(long id)
        {
            Note note = db.Notes.Find(id);
            if (note != null)
                db.Notes.Remove(note);
        }

        public IEnumerable<Note> Find(Func<Note, bool> predicate)
        {
            return db.Notes.Where(predicate).ToList();
        }

        public Note Get(long id)
        {
            return db.Notes.Find(id);
        }

        public IEnumerable<Note> GetAll()
        {
            return db.Notes;
        }

        public void Update(Note item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Notes.Update(item);
        }
    }
}
