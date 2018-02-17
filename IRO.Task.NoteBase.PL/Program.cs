using System;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.BLL.Core;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUserLogic userLogic = new UserLogic();
            IBookLogic bookLogic = new BookLogic(userLogic.GetAll());
            INoteLogic noteLogic = new NoteLogic(bookLogic.GetAll());
            
            string[] input;
            DisplayCommands();
            do
            {
                Console.Write(">");
                input = CommandLineParser.Parse(Console.ReadLine()?.ToLower());
                switch (input[0])
                {
                    case "adduser":
                        {
                            AddUser(userLogic, input[1]);
                            break;
                        }
                    case "login":
                        {
                            Login(userLogic, input[1]);
                            break;
                        }
                    case "userslist":
                        {
                            UsersList(userLogic);
                            break;
                        }
                    case "addnote":
                        {
                            AddNote(noteLogic, bookLogic, userLogic, input[2], input[1]);
                            break;
                        }
                    case "showallnotes":
                        {
                            NotesList(bookLogic, noteLogic, userLogic, input[1]);
                            break;
                        }
                    case "addbook":
                        {
                            AddBook(bookLogic, userLogic, input[1]);
                            break;
                        }
                    case "books":
                        {
                            BooksList(bookLogic, userLogic);
                            break;
                        }
                    case "deleteuser":
                        {
                            DeleteUser(userLogic);
                            break;
                        }
                    case "deletebook":
                        {
                            DeleteBook(bookLogic, userLogic, input[1]);
                            break;
                        }
                    case "deletenote":
                        {
                            DeleteNote(bookLogic, noteLogic, userLogic, input[1]);
                            break;
                        }
                    case "commands":
                    case "help":
                        {
                            DisplayCommands();
                            break;
                        }
                    case "clear":
                        {
                            Console.Clear();
                            break;
                        }
                    case "quit":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Ошибка выбора!");
                            break;
                        }
                }
            }
            while (input[0] != "quit");
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("addUser [\"userName\"]\t\t- добавление пользователя в программу\n" +
                              "login [userId]\t\t\t- вход под пользователем из списка\n" +
                              "userslist\t\t\t- вывести имена всех пользователей\n" +
                              "addnote [bookId] [\"noteText\"]\t- добавить записку в книгу(Нужна авторизация)\n" +
                              "deleteNote [noteId]\t\t- удалить записку из книги(Нужна авторизация)\n" +
                              "notesList\t\t\t- вывести все записки из книги(Нужна авторизация)\n" +
                              "addBook [\"bookName\"]\t\t- добавить новую книгу (Нужна авторизация)\n" +
                              "deleteBook [bookId]\t\t- удалить книгу(Нужна авторизация)\n" +
                              "booksList\t\t\t- вывести Id всех книг(Нужна авторизация)\n" +
                              "quit\t\t\t\t- выйти из приложения.");
        }

        private static void AddUser(IUserLogic userLogic, string name)
        {
            if (name == string.Empty)
            {
                Console.WriteLine("Имя некорректно!");
                return;
            }
            User user = new User
            {
                Name = name,
            };
            Console.WriteLine(userLogic.AddUser(user)
                ? "Пользователь успешно добавлен."
                : "Во время добавления пользователя произошла ошибка!");
        }

        private static void UsersList(IUserLogic userLogic)
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

        private static void Login(IUserLogic userLogic, string userId)
        {
            if (!long.TryParse(userId, out long id))
            {
                Console.WriteLine("Id пользователя некорректно!");
                return;
            }

            Console.WriteLine(userLogic.Login(id)
                ? $"Вы успешно зашли, {userLogic.ActiveUser.Name}."
                : "Пользователь не найден!");
        }

        private static void AddNote(INoteLogic noteLogic, IBookLogic bookLogic, IUserLogic userLogic, string noteText, string bookId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением записки вы должны быть авторизованы!");
                return;
            }

            if (noteText == string.Empty)
            {
                Console.WriteLine("Текст некорректен!");
                return;
            }

            if (!long.TryParse(bookId, out long BookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(BookId);
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

        private static void NotesList(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic, string bookId)
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

            foreach(var note in notes)
            {
                Console.WriteLine($"id:{note.Id}\tname:{note.Text}");
            }
        }

        private static void DeleteNote(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic, string noteId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления записки вы должны быть авторизованы!");
                return;
            }
            if (!int.TryParse(noteId, out int NoteId))
            {
                Console.WriteLine("Id записки некорректно!");
                return;
            }

            var note = noteLogic.GetById(NoteId);
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

            Console.WriteLine(noteLogic.DeleteNote(NoteId)
                ? "Записка успешно удалена."
                : "Во время удаления записки произошла ошибка!");
        }

        private static void AddBook(IBookLogic bookLogic, IUserLogic userLogic, string bookName)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением книги вы должны быть авторизованы!");
                return;
            }

            if (bookName == string.Empty)
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

        private static void BooksList(IBookLogic bookLogic, IUserLogic userLogic)
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

        private static void DeleteBook(IBookLogic bookLogic, IUserLogic userLogic, string bookId)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления книги вы должны быть авторизованы!");
                return;
            }

            if (!int.TryParse(bookId, out int BookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(BookId);
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

            Console.WriteLine(bookLogic.DeleteBook(BookId)
                ? "Книга успешно удалена."
                : "Во время удаления книги произошла ошибка!");
        }

        private static void DeleteUser(IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления учётной записи вы должны быть авторизованы!");
                return;
            }
            Console.WriteLine("Вы уверены, что хотите удалить свою учётную запись?");
            var response = Console.ReadLine()?.ToLower()[0]; 
            if (response == 'y' || response == 'д')
            {
                Console.WriteLine(userLogic.DeleteUser(userLogic.ActiveUser.Id)
                ? "Вы удалили свою учётную запись."
                : "Во время удаления вашей учётной записи произошла ошибка!");
            }
        }
    }
}
