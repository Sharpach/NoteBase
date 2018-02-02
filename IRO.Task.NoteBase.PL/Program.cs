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
            do
            {
                Console.Clear();
                if (userLogic.ActiveUser != null) Console.WriteLine($"Добро пожаловать, {userLogic.ActiveUser.Name}");
                DisplayCommands();
                input = Console.ReadLine();
                switch (input)
                {
                    case "AddUser":
                        {
                            Console.Clear();
                            AddUser(userLogic);
                            break;
                        }
                    case "Login":
                        {
                            Console.Clear();
                            Login(userLogic);
                            break;
                        }
                    case "List":
                        {
                            Console.Clear();
                            List(userLogic);
                            break;
                        }
                    case "AddNote":
                        {
                            Console.Clear();
                            break;
                        }
                    case "ShowAllNotes":
                        {
                            Console.Clear();
                            break;
                        }
                    case "Books":
                        {
                            Console.Clear();
                            break;
                        }
                        case "Quit":
                        {
                            return;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка выбора!");
                            Console.ReadLine();
                            break;
                        }
                }
            }

            while (input != "Quit");
        }

        private static void DisplayCommands()
        {            Console.WriteLine("AddUser - Добавление пользователя в программу\n" +
                              "Login - вход под пользователем из списка\n" +
                              "List - вывести имена всех пользователей\n" +
                              "AddNote - добавить записку в книгу\n" +
                              "ShowAllNotes - вывести все записки из книги\n" +
                              "Books - вывести ID всех книг");
        }

        private static void AddUser(IUserLogic userLogic)
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            if (name == string.Empty)
            {
                Console.WriteLine("Имя не корректно!");
                Console.ReadLine();
                return;
            }
            var user = new User
            {
                Name = name,
            };
            userLogic.AddUser(user);
        }

        private static void List(IUserLogic userLogic)
        {
            List<User> users = userLogic.List();
            if (users.Count < 1)
            {
                Console.WriteLine("Пользователей нет!");
                Console.ReadLine();
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"id:{user.Id} name:{user.Name}");
            }
            Console.ReadLine();
            return;
        }

        private static void Login(IUserLogic userLogic)
        {
            Console.Write("Введите айди пользователя для входа: ");
            if (!uint.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID не может быть меньше нуля");
                Console.ReadLine();
                return;
            }
            if(userLogic.Login(id))
            {
                Console.WriteLine($"Вы успешно зашли, {userLogic.ActiveUser.Name}");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Во время входа произошла ошибка!");
                Console.ReadLine();
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
