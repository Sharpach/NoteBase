using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.BLL.Core;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IRO.Task.NoteBase.PL.ConsoleApp
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=taskmanagerdb;Trusted_Connection=True;";

            var services = new ServiceCollection()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IBookService, BookService>()
                .AddTransient<INoteService, NoteService>()
                .AddTransient<IUnitOfWork, EFUnitOfWork>()
                .AddTransient<IMainContext, MainContext>(contextProvider => { return new MainContext(connectionString); });

            var serviceProvider = services.BuildServiceProvider();

            var userService = serviceProvider.GetService<IUserService>();
            var bookService = serviceProvider.GetService<IBookService>();
            var noteService = serviceProvider.GetService<INoteService>();

            DisplayCommands();
            RunCycle(userService, bookService, noteService);
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("addUser [\"userName\"]\t\t\t- добавление пользователя в программу\n" +
                              "login [userId]\t\t\t\t- вход под пользователем из списка\n" +
                              "userslist\t\t\t\t- вывести имена всех пользователей\n" +
                              "deleteuser\t\t\t\t- удалить свою учётную запись (Нужна авторизация)\n" +
                              "addnote [bookId] [\"noteText\"]\t\t- добавить записку в книгу (Нужна авторизация)\n" +
                              "changeNote [noteId] [\"new noteName\"]\t- изменить название записки (Нужна авторизация)\n" +
                              "deleteNote [noteId]\t\t\t- удалить записку из книги (Нужна авторизация)\n" +
                              "notesList\t\t\t\t- вывести все записки из книги (Нужна авторизация)\n" +
                              "addBook [\"bookName\"]\t\t\t- добавить новую книгу (Нужна авторизация)\n" +
                              "changeBook [bookId] [\"new bookName\"]\t- изменить название книги (Нужна авторизация)\n" +
                              "deleteBook [bookId]\t\t\t- удалить книгу(Нужна авторизация)\n" +
                              "booksList\t\t\t\t- вывести Id всех книг(Нужна авторизация)\n" +
                              "quit\t\t\t\t\t- выйти из приложения.");
        }

        private static void RunCycle(IUserService userService, IBookService bookService, INoteService noteService)
        {
            string[] input;

            do
            {
                Console.Write(">");
                input = CommandLineParser.Parse(Console.ReadLine(), 3);
                switch (input[0]?.ToLower())
                {
                    case "adduser":
                        {
                            AddUser(userService, input[1]);
                            break;
                        }
                    case "login":
                        {
                            Login(userService, input[1]);
                            break;
                        }
                    case "userslist":
                        {
                            UsersList(userService);
                            break;
                        }
                    case "deleteuser":
                        {
                            DeleteUser(userService, bookService, noteService);
                            break;
                        }
                    case "addnote":
                        {
                            AddNote(noteService, bookService, userService, input[1], input[2]);
                            break;
                        }
                    case "noteslist":
                        {
                            NotesList(bookService, noteService, userService, input[1]);
                            break;
                        }
                    case "changenote":
                        {
                            ChangeNote(bookService, noteService, userService, input[1], input[2]);
                            break;
                        }
                    case "deletenote":
                        {
                            DeleteNote(bookService, noteService, userService, input[1]);
                            break;
                        }
                    case "addbook":
                        {
                            AddBook(bookService, userService, input[1]);
                            break;
                        }
                    case "bookslist":
                        {
                            BooksList(bookService, userService);
                            break;
                        }
                    case "changebook":
                        {
                            ChangeBook(bookService, userService, input[1], input[2]);
                            break;
                        }
                    case "deletebook":
                        {
                            DeleteBook(bookService, userService, input[1]);
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
    }
}
