using System;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.BLL.Core;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.PL
{
    internal partial class Program
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
                input = CommandLineParser.Parse(Console.ReadLine(), 3);
                switch (input[0].ToLower())
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
                            AddNote(noteLogic, bookLogic, userLogic, input[1], input[2]);
                            break;
                        }
                    case "noteslist":
                        {
                            NotesList(bookLogic, noteLogic, userLogic, input[1]);
                            break;
                        }
                    case "addbook":
                        {
                            AddBook(bookLogic, userLogic, input[1]);
                            break;
                        }
                    case "bookslist":
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
                              "deleteuser\t\t\t- удалить свою учётную запись(Нужна авторизация)\n" +
                              "addnote [bookId] [\"noteText\"]\t- добавить записку в книгу(Нужна авторизация)\n" +
                              "deleteNote [noteId]\t\t- удалить записку из книги(Нужна авторизация)\n" +
                              "notesList\t\t\t- вывести все записки из книги(Нужна авторизация)\n" +
                              "addBook [\"bookName\"]\t\t- добавить новую книгу (Нужна авторизация)\n" +
                              "deleteBook [bookId]\t\t- удалить книгу(Нужна авторизация)\n" +
                              "booksList\t\t\t- вывести Id всех книг(Нужна авторизация)\n" +
                              "quit\t\t\t\t- выйти из приложения.");
        }
    }
}
