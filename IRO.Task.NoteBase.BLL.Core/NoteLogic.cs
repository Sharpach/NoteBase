using IRO.Task.NoteBase.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.Memory;

namespace IRO.Task.NoteBase.BLL.Core
{
    class NoteLogic : INoteLogic
    {
        private readonly NoteDao _noteDao;

        public NoteLogic()
        {
            _noteDao = new NoteDao();
        }
        public bool AddNote(Note note) => _noteDao.AddNote(note);

        public List<Note> GetAll() => _noteDao.GetAll();

        public Note GetByID(uint noteID) => _noteDao.GetByID(noteID);
    }
}
