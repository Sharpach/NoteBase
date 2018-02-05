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
            INoteLogic noteLogic = new NoteLogic();
            IBookLogic bookLogic = new BookLogic();
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
                     "ShowAllNotes\t- вывести все записки из книги(Нужна авторизация)\n" +
                     "Books\t\t- вывести Id всех книг(Нужна авторизация)\n" +
                     "AddBook\t\t- добавить новую книгу (Нужна авторизация)\n");
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
            List<User> users = userLogic.GetAll();
            if (users.Count < 1)
            {
                Console.WriteLine("Пользователей нет!");
                return;
            }
            foreach (User user in users)
            {
                Console.WriteLine($"id:{user.Id}\tname:{user.Name}");
            }
        }

        private static void Login(IUserLogic userLogic)
        {
            Console.Write("Введите айди пользователя для входа: ");
            if (!uint.TryParse(Console.ReadLine(), out uint id))
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

            Console.WriteLine("Введите Id книги: ");
            if (!uint.TryParse(Console.ReadLine(), out uint bookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            Book book = bookLogic.GetById(bookId);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.Owner != userLogic.ActiveUser)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            Note note = new Note
            {
                Text = text,
                ParentBook = book
            };

            Console.WriteLine(noteLogic.AddNote(note)
                ? "Записка успешно добавлена."
                : "Во время добавления записки произошла ошибка!");
        }

        private static void ShowAllNotes(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед просмотра списка записок вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите Id книги, записки из которой хотите узнать: ");
            if (!uint.TryParse(Console.ReadLine(), out uint bookId))
            {
                Console.WriteLine("Id книги некорректно!");
                return;
            }

            Book book = bookLogic.GetById(bookId);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }

            if (book.Owner != userLogic.ActiveUser)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }

            List<Note> notes = noteLogic.GetByBook(book);
            if (notes.Count < 1)
            {
                Console.WriteLine("Записок нет!");
                return;
            }

            foreach(Note note in notes)
            {
                Console.WriteLine($"id:{note.Id}\tname:{note.Text}");
            }
        }

        private static void AddBook(IBookLogic bookLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением книги вы должны быть авторизованы!");
                return;
            }

            Console.Write("Введите айди книги: ");
            string name = Console.ReadLine();
            if (name == string.Empty)
            {
                Console.WriteLine("Название некорректно!");
                return;
            }

            User user = userLogic.GetById(userLogic.ActiveUser.Id);
            Book book = new Book
            {
                Name = name,
                Owner = user
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

            List<Book> books = bookLogic.GetByUser(userLogic.ActiveUser);
            if (books.Count < 1)
            {
                Console.WriteLine("Книг нет!");
                return;
            }

            foreach (Book book in books)
            {
                Console.WriteLine($"id:{book.Id}\tname:{book.Name}");
            }
        }
    }
}
