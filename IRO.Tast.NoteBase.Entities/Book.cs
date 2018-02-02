using System.Collections.Generic;

namespace IRO.Task.NoteBase.Entities
{
    public class Book
    {
        public uint Id;
        public List<Note> Notes;
        public User Owner; 
    }
}
