using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Note GetByID(uint NoteId)
        {
            return Memory.Find(x => x.Id == NoteId);
        }

        public List<Note> GetNotesList()
        {
            return Memory;
        }
    }
}
