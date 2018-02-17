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
            
            string input;
            DisplayCommands();
            do
            {
                Console.Write(">");
                input = Console.ReadLine()?.ToLower();
                switch (input)
                {
                    case "adduser":
                        {
                            AddUser(userLogic);
                            break;
                        }
                    case "login":
                        {
                            Login(userLogic);
                            break;
                        }
                    case "list":
                        {
                            List(userLogic);
                            break;
                        }
                    case "addnote":
                        {
                            AddNote(noteLogic, bookLogic, userLogic);
                            break;
                        }
                    case "showallnotes":
                        {
                            ShowAllNotes(bookLogic, noteLogic, userLogic);
                            break;
                        }
                    case "addbook":
                        {
                            AddBook(bookLogic, userLogic);
                            break;
                        }
                    case "books":
                        {
                            Books(bookLogic, userLogic);
                            break;
                        }
                    case "deleteuser":
                        {
                            DeleteUser(userLogic);
                            break;
                        }
                    case "deletebook":
                        {
                            DeleteBook(bookLogic, userLogic);
                            break;
                        }
                    case "deletenote":
                        {
                            DeleteNote(bookLogic, noteLogic, userLogic);
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
            while (input != "quit");
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("AddUser\t\t- добавление пользователя в программу\n" +
                     "Login\t\t- вход под пользователем из списка\n" +
                     "List\t\t- вывести имена всех пользователей\n" +
                     "AddNote\t\t- добавить записку в книгу(Нужна авторизация)\n" +
                     "DeleteNote\t- удалить записку из книги(Нужна авторизация)\n" +
                     "ShowAllNotes\t- вывести все записки из книги(Нужна авторизация)\n" +
                     "Books\t\t- вывести Id всех книг(Нужна авторизация)\n" +
                     "AddBook\t\t- добавить новую книгу (Нужна авторизация)\n" +
                     "DeleteBook\t- удалить книгу(Нужна авторизация)\n" +
                     "Quit\t\t- выйти из приложения.");
        }

        private static void AddUser(IUserLogic userLogic)
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
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

        private static void List(IUserLogic userLogic)
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

        private static void Login(IUserLogic userLogic)
        {
            Console.Write("Введите айди пользователя для входа: ");
            if (!long.TryParse(Console.ReadLine(), out long id))
            {
                Console.WriteLine("Id пользователя некорректно!");
                return;
            }

            Console.WriteLine(userLogic.Login(id)
                ? $"Вы успешно зашли, {userLogic.ActiveUser.Name}."
                : "Пользователь не найден!");
        }

        private static void AddNote(INoteLogic noteLogic, IBookLogic bookLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением записки вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите текст записки: ");
            string text = Console.ReadLine();
            if (text == string.Empty)
            {
                Console.WriteLine("Текст некорректен!");
                return;
            }

            Console.Write("Введите Id книги: ");
            if (!long.TryParse(Console.ReadLine(), out long bookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(bookId);
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
                Text = text,
                ParentBookId = book.Id
            };

            Console.WriteLine(noteLogic.AddNote(note)
                ? "Записка успешно добавлена."
                : "Во время добавления записки произошла ошибка!");
        }

        private static void ShowAllNotes(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед просмотром списка записок вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите Id книги, записки из которой хотите узнать: ");
            string stringBookId = Console.ReadLine();
            if (!long.TryParse(stringBookId, out long bookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(bookId);
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

        private static void DeleteNote(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления записки вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите Id записки, которую хотите удалить: ");
            if (!int.TryParse(Console.ReadLine(), out int noteId))
            {
                Console.WriteLine("Id записки некорректно!");
                return;
            }

            var note = noteLogic.GetById(noteId);
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

            Console.WriteLine(noteLogic.DeleteNote(noteId)
                ? "Записка успешно удалена."
                : "Во время удаления записки произошла ошибка!");
        }

        private static void AddBook(IBookLogic bookLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением книги вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите имя книги: ");
            string name = Console.ReadLine();
            if (name == string.Empty)
            {
                Console.WriteLine("Название некорректно!");
                return;
            }

            var user = userLogic.GetById(userLogic.ActiveUser.Id);
            Book book = new Book
            {
                Name = name,
                OwnerId = user.Id
            };

            Console.WriteLine(bookLogic.AddBook(book)
                ? "Книга успешно добавлена."
                : "Во время добавления книги произошла ошибка!");
        }

        private static void Books(IBookLogic bookLogic, IUserLogic userLogic)
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

        private static void DeleteBook(IBookLogic bookLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для удаления книги вы должны быть авторизованы!");
                return;
            }

            Console.WriteLine("Введите Id книги: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            var book = bookLogic.GetById(bookId);
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

            Console.WriteLine(bookLogic.DeleteBook(bookId)
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
