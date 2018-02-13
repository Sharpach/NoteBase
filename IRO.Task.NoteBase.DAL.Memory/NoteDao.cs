using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Memory
{
    public class NoteDao : INoteDao
    {
        private List<Note> Memory;
        public NoteDao()
        {
            Memory = new List<Note>();
        }
        public bool AddNote(Note note)
        {
            if (Memory.Count == 0)
                note.Id = 0;
            else
                note.Id = Memory[Memory.Count - 1].Id + 1;
            Memory.Add(note);
            return true;
        }
        public Note GetById(int NoteId) => Memory.Find(x => x.Id == NoteId);
        public List<Note> GetByBook(Book book) => Memory.FindAll(x => x.ParentBook == book);
        public List<Note> GetAll() => Memory;
    }
}
