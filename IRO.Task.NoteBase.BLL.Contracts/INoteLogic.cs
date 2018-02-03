using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface INoteLogic
    {
        bool AddNote(Note note);
        List<Note> GetAll();
        Book GetNoteByID(uint noteID);
    }
}