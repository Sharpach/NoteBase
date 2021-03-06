using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.Entities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.PL.ConsoleApp
{
    internal partial class Program
    {
        private static void AddUser(IUserService userLogic, string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя некорректно!");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Пароль не может быть пустым!");
                return;
            }
            
            Console.WriteLine(userLogic.AddUser(name, password)
                ? "Пользователь успешно добавлен."
                : "Во время добавления пользователя произошла ошибка!");
        }

        private static void UsersList(IUserService userLogic)
        {
            var users = userLogic.GetAll();
            if (users.Count < 1)
            {
                Console.WriteLine("Пользователей нет!");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"id:{user.Id}\tname:{user.Name}");
            }
        }

        private static void Login(IUserService userLogic, string name, string password)
        {
            User user = userLogic.GetByName(name);

            Console.WriteLine(userLogic.Login(user.Id, password)
            ? $"Вы успешно зашли, {userLogic.ActiveUser.Name}."
            : "Пароль неверный!");
        }

        private static void AddNote(INoteService noteLogic, IBookService bookLogic, IUserService userLogic, string bookId, string noteText)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением записки вы должны быть авторизованы!");
                return;
            }

            if (String.IsNullOrWhiteSpace(noteText))
            {
                Console.WriteLine("Текст некорректен!");
                return;
            }

            if (!long.TryParse(bookId, out long id))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            Note note = new Note
            {
                Text = noteText,
                ParentBookId = book.Id
            };

            Console.WriteLine(noteLogic.AddNote(note)
                ? "Записка успешно добавлена."
                : "Во время добавления записки произошла ошибка!");
        }

        private static void NotesList(IBookService bookLogic, INoteService noteLogic, IUserService userLogic, string bookId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед просмотром списка записок вы должны быть авторизованы!");
                return;
            }

            if (!long.TryParse(bookId, out long longBookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(longBookId);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            var notes = noteLogic.GetByBook(book);
            if (notes.Count < 1)
            {
                Console.WriteLine("Записок нет!");
                return;
            }

            foreach (var note in notes)
            {
                Console.WriteLine($"id:{note.Id}\tname:{note.Text}");
            }
        }

        private static void ChangeNote(IBookService bookLogic, INoteService noteLogic, IUserService userLogic, string noteId, string newText)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для изменения записки вы должны быть авторизованы!");
                return;
            }
            if (!long.TryParse(noteId, out long Id))
            {
                Console.WriteLine("Id записки некорректно!");
                return;
            }

            var note = noteLogic.GetById(Id);
            if (note == null)
            {
                Console.WriteLine("Записка не найдена!");
                return;
            }

            var book = bookLogic.GetById(note.ParentBookId);
            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Записка не принадлежит вам.");
                return;
            }

            Console.WriteLine(noteLogic.ChangeNote(Id, newText)
                ? "Записка успешно изменена."
                : "Во время изменения записки произошла ошибка!");
        }

        private static void DeleteNote(IBookService bookLogic, INoteService noteLogic, IUserService userLogic, string noteId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления записки вы должны быть авторизованы!");
                return;
            }
            if (!long.TryParse(noteId, out long Id))
            {
                Console.WriteLine("Id записки некорректно!");
                return;
            }

            var note = noteLogic.GetById(Id);
            if (note == null)
            {
                Console.WriteLine("Записка не найдена!");
                return;
            }

            var book = bookLogic.GetById(note.ParentBookId);
            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Записка не принадлежит вам.");
                return;
            }

            Console.WriteLine(noteLogic.DeleteNote(Id)
                ? "Записка успешно удалена."
                : "Во время удаления записки произошла ошибка!");
        }

        private static void AddBook(IBookService bookLogic, IUserService userLogic, string bookName)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением книги вы должны быть авторизованы!");
                return;
            }

            if (string.IsNullOrWhiteSpace(bookName))
            {
                Console.WriteLine("Название некорректно!");
                return;
            }

            var user = userLogic.GetById(userLogic.ActiveUser.Id);
            Book book = new Book
            {
                Name = bookName,
                OwnerId = user.Id
            };

            Console.WriteLine(bookLogic.AddBook(book)
                ? "Книга успешно добавлена."
                : "Во время добавления книги произошла ошибка!");
        }

        private static void BooksList(IBookService bookLogic, IUserService userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для вывода списка всех книг вы должны быть авторизованы!");
                return;
            }

            var books = bookLogic.GetByUser(userLogic.ActiveUser);
            if (books.Count < 1)
            {
                Console.WriteLine("Книг нет!");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"id:{book.Id}\tname:{book.Name}");
            }
        }

        private static void ChangeBook(IBookService bookLogic, IUserService userLogic, string bookId, string newName)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для изменения названия книги вы должны быть авторизованы!");
                return;
            }

            if (!long.TryParse(bookId, out long id))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            Console.WriteLine(bookLogic.ChangeBook(id, newName)
                ? "Книга успешно удалена."
                : "Во время удаления книги произошла ошибка!");
        }

        private static void DeleteBook(IBookService bookLogic, IUserService userLogic, string bookId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления книги вы должны быть авторизованы!");
                return;
            }

            if (!long.TryParse(bookId, out long id))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.OwnerId != userLogic.ActiveUser.Id)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            Console.WriteLine(bookLogic.DeleteBook(id)
                ? "Книга успешно удалена."
                : "Во время удаления книги произошла ошибка!");
        }

        private static void DeleteUser(IUserService userLogic, IBookService bookLogic, INoteService noteLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления учётной записи вы должны быть авторизованы!");
                return;
            }
            Console.WriteLine("Вы уверены, что хотите удалить свою учётную запись?");
            var response = Console.ReadLine()?.ToLower()[0];
            if (response != 'y' && response != 'д') return;

            var user = userLogic.ActiveUser;
            var removeBooks = bookLogic.GetByUser(user);
            noteLogic.DeleteNotes(removeBooks);
            bookLogic.DeleteAllBooksByUser(user);
            Console.WriteLine(userLogic.DeleteUser(userLogic.ActiveUser.Id)
                ? "Вы удалили свою учётную запись."
                : "Во время удаления вашей учётной записи произошла ошибка!");
        }
    }
}