using System;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.BLL.Core;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUserLogic userLogic = new UserLogic();
            //INoteLogic noteLogic = new NoteLogic();
            //IBookLogic bookLogic = new BookLogic();

            do
            {
                Console.Clear();
                DisplayCommands();
                switch (Console.ReadLine())
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
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Ошибка выбора!");
                            break;
                        }
                }
            }

            while (Console.ReadKey().Key != ConsoleKey.Q);
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("AddUser - Добавление пользователя в программу\n" +
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
            var users = userLogic.List();
            if (users.Count < 1)
            {
                Console.WriteLine("Пользователей нет!");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"id:{user.Id} name:{user.Name}");
            }
        }

        //private static void Login(IUserLogic userLogic)
        //{

        //}

        //private static void AddNote(INoteLogic noteLogic)
        //{

        //}

        //private static void ShowAllNotes(IBookLogic noteLogic)
        //{

        //}

        //private static void Books(IBookLogic bookLogic)
        //{

        //}
    }
}
