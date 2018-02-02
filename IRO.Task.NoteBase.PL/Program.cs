using System;

namespace IRO.Task.NoteBase.PL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            do
            {
                DisplayCommands();
                switch (Console.ReadLine())
                {
                    case "AddUser":
                        {
                            Console.Clear();
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

    }
}
