using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.Memory;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class NoteLogic : INoteLogic
    {
        private readonly NoteDao _noteDao;

        public NoteLogic()
        {
            _noteDao = new NoteDao();
        }
        public bool AddNote(Note note) => _noteDao.AddNote(note);

        public List<Note> GetAll() => _noteDao.GetAll();
        public List<Note> GetByBook(Book book) => _noteDao.GetByBook(book);
        public Note GetById(uint noteId) => _noteDao.GetById(noteId);
    }
}
