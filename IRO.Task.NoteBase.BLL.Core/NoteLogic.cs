﻿using IRO.Task.NoteBase.BLL.Contracts;
using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.DAL.EF;
using System.Data.Entity;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class NoteLogic : INoteLogic
    {
        //TODO: intefraces EF
        private readonly MainContext _context;
        private readonly DbSet<Note> _dbSet;

        public NoteLogic()
        {
            _context = new MainContext();
            _dbSet = _context.Notes;
        }
        public bool AddNote(Note note)
        {
            _dbSet.Add(note);
            _context.SaveChanges();
            return true;
        }

        public List<Note> GetAll() => _dbSet.ToList();
        public List<Note> GetByBook(Book book) => _dbSet.Where(x => x.ParentBook.Id == book.Id).ToList();
        public Note GetById(int noteId) => _dbSet.FirstOrDefault(x => x.Id == noteId);
    }
}
