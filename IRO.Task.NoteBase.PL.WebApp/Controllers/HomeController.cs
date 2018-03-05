using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IRO.Task.NoteBase.PL.WebApp.Models;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.BLL.Contracts;
using AutoMapper;

namespace IRO.Task.NoteBase.PL.WebApp.Controllers
{
    public class HomeController : Controller
    {
        IBookService bookService;
        INoteService noteService;
        IMapper mapper;

        public HomeController(IBookService bookService, INoteService noteService, IMapper mapper)
        {
            this.bookService = bookService;
            this.noteService = noteService;
            this.mapper = mapper;
        }

        #region Books

        public IActionResult Index()
        {
            var books = bookService.GetAll();
            var booksVM = mapper.Map<IEnumerable<Book>, List<BookViewModel>>(books);
            return View(booksVM);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return PartialView("_AddBook", new BookViewModel { OwnerId = 1 });
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel model)
        {
            var book = mapper.Map<BookViewModel, Book>(model);
            if (ModelState.IsValid)
            {
                bookService.AddBook(book);
            }            
            if (book.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View("_AddBook", model);
        }

        [HttpGet]
        public IActionResult EditBook(long? id)
        {
            BookViewModel model = new BookViewModel();
            if (id.HasValue && id != 0)
            {
                Book book = bookService.GetById(id.Value);
                model = mapper.Map<Book, BookViewModel>(book);
            }
            return PartialView("_EditBook", model);
        }

        [HttpPost]
        public IActionResult EditBook(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                bookService.ChangeBook(model.Id, model.Name);
            }
            if (model.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteBook(long? id)
        {
            BookViewModel model = new BookViewModel();
            if (id.HasValue && id != 0)
            {
                Book book = bookService.GetById(id.Value);
                model = mapper.Map<Book, BookViewModel>(book);
            }
            return PartialView("_DeleteBook", model.Name);
        }

        [HttpPost]
        public IActionResult DeleteBook(long id)
        {
            bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }

        public IActionResult OpenBook(long? id)
        {
            if (!id.HasValue) return Error();
            var book = bookService.GetById(id.Value);
            if (book == null) return Error();

            var notes = noteService.GetByBook(book);
            var notesVM = mapper.Map<IEnumerable<Note>, List<NoteViewModel>>(notes);

            var model = mapper.Map<Book, BookViewModel>(book);
            model.Notes = notesVM;

            return View("BookNotes", model);
        }

        #endregion

        #region Notes

        [HttpGet]
        public IActionResult AddNote(long bookId)
        {
            return PartialView("_AddNote", new NoteViewModel { ParentBookId = bookId });
        }

        [HttpPost]
        public IActionResult AddNote(NoteViewModel model)
        {
            var note = mapper.Map<NoteViewModel, Note>(model);
            if (ModelState.IsValid)
            {
                noteService.AddNote(note);
            }
            if (note.Id > 0)
            {
                return Redirect($"/Home/OpenBook/{note.ParentBookId}");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditNote(long? id)
        {
            NoteViewModel model = new NoteViewModel();
            if (id.HasValue && id != 0)
            {
                Note note = noteService.GetById(id.Value);
                model = mapper.Map<Note, NoteViewModel>(note);
            }
            return PartialView("_EditNote", model);
        }

        [HttpPost]
        public IActionResult EditNote(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                noteService.ChangeNote(model.Id, model.Text);
            }
            if (model.Id > 0)
            {
                return Redirect($"/Home/OpenBook/{model.ParentBookId}");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteNote(long? id)
        {
            NoteViewModel model = new NoteViewModel();
            if (id.HasValue && id != 0)
            {
                Note note = noteService.GetById(id.Value);
                model = mapper.Map<Note, NoteViewModel>(note);
            }
            return PartialView("_DeleteNote", model.Text);
        }

        [HttpPost]
        public IActionResult DeleteNote(long id)
        {
            var bookId = noteService.GetById(id).ParentBookId;
            noteService.DeleteNote(id);
            return Redirect($"/Home/OpenBook/{bookId}");
        }

        #endregion

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
