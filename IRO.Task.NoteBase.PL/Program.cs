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
            //IBookLogic bookLogic = new BookLogic();
            string input = string.Empty;
            //if (userLogic.ActiveUser != null) Console.WriteLine($"Добро пожаловать, {userLogic.ActiveUser.Name}");
            DisplayCommands();
            do
            {
                Console.Write(">");
                input = Console.ReadLine().ToLower();
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
                            break;
                        }
                    case "showallnotes":
                        {
                            break;
                        }
                    case "books":
                        {
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
        {            Console.WriteLine("AddUser\t\t- добавление пользователя в программу\n" +
                              "Login\t\t- вход под пользователем из списка\n" +
                              "List\t\t- вывести имена всех пользователей\n" +
                              "AddNote\t\t- добавить записку в книгу\n" +
                              "ShowAllNotes\t- вывести все записки из книги\n" +
                              "Books\t\t- вывести ID всех книг");
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
            var user = new User
            {
                Name = name,
            };
            if(userLogic.AddUser(user))
            {
                Console.WriteLine("Пользователь успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Во время добавления пользователя произошла ошибка!");
            }
        }

        private static void List(IUserLogic userLogic)
        {
            List<User> users = userLogic.GetAll();
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
            if (!uint.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID пользователя некорректно!");
                return;
            }
            if(userLogic.Login(id))
            {
                Console.WriteLine($"Вы успешно зашли, {userLogic.ActiveUser.Name}.");
            }
            else
            {
                Console.WriteLine("Пользователь не найден!");
            }
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
            Console.WriteLine("Введите ID книги: ");
            if (!uint.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("ID книги некорректно!");
                return;
            }
            Book book = bookLogic.GetByID(bookId);
            if(book.Owner != userLogic.ActiveUser)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }
            if (text == string.Empty)
            {
                Console.WriteLine("Текст некорректен!");
                return;
            }
            var note = new Note
            {
                Text = text,
                ParentBook = book
            };
            if (noteLogic.AddNote(note))
            {
                Console.WriteLine("Записка успешно добавлена.");
            }
            else
            {
                Console.WriteLine("Во время добавления записки произошла ошибка!");
            }
        }

        private static void ShowAllNotes(IBookLogic bookLogic, INoteLogic noteLogic, IUserLogic userLogic)
        {
            if (userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед просмотра списка записок вы должны быть авторизованы!");
                return;
            }
            Console.WriteLine("Введите ID книги, записки из которой хотите узнать: ");
            if (!uint.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("ID книги некорректно!");
                return;
            }
            Book book = bookLogic.GetByID(bookId);
            if (book.Owner != userLogic.ActiveUser)
            {
                Console.WriteLine("Книга не принадлежит вам.");
                return;
            }
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }
            List<Note> notes = noteLogic.GetAll();
            if (notes.Count < 1)
            {
                Console.WriteLine("Записок нет!");
                return;
            }
            foreach (Note note in notes.FindAll(x => x.ParentBook == book))
            {
                Console.WriteLine($"id:{note.Id}\tname:{note.Text}");
            }
        }

        private static void AddBook(IBookLogic bookLogic, IUserLogic userLogic)
        {
            if(userLogic.ActiveUser == null)
            {
                Console.WriteLine("Перед добавлением книги вы должны быть авторизованы!");
                return;
            }
            Console.Write("Введите название книги: ");
            string name = Console.ReadLine();
            User user = userLogic.GetByID(userLogic.ActiveUser.Id);
            if (name == string.Empty)
            {
                Console.WriteLine("Название некорректно!");
                return;
            }
            var book = new Book
            {
                Name = name,
                Owner = user
            };
            if (bookLogic.AddBook(book))
            {
                Console.WriteLine("Книга успешно добавлена.");
            }
            else
            {
                Console.WriteLine("Во время добавления книги произошла ошибка!");
            }
        }

        private static void Books(IBookLogic bookLogic, IUserLogic userLogic)
        {
            if(userLogic.ActiveUser == null)
            {
                Console.WriteLine("Для вывода списка всех книг вы должны быть авторизованы!");
                return;
            }
            List<Book> books = bookLogic.GetAll();
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
