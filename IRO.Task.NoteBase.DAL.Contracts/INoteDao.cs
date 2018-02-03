using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface INoteDao
    {
        bool AddNote(Note note);
        Note GetByID(uint noteID);
        List<Note> GetNotesList();
    }
}
