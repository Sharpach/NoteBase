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
            //INoteLogic noteLogic = new NoteLogic();
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
            return;
        }

        private static void Login(IUserLogic userLogic)
        {
            Console.Write("Введите айди пользователя для входа: ");
            if (!uint.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID некорректно!");
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

        //private static void AddNote(INoteLogic noteLogic)
        //{

        //}

        //private static void ShowAllNotes(IBookLogic booklogic)
        //{

        //}

        //private static void AddBook(IBookLogic booklogic)
        //{

        //}

        //private static void Books(IBookLogic bookLogic)
        //{

        //}
    }
}
