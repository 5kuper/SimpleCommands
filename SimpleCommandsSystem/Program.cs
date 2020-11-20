using System;
using SimpleCommandsSystem.CommandLists;

namespace SimpleCommandsSystem
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Simple Commands System";

            // Setting standard values
            Command.StandardPrefix = "!";
            Command.StandardDescription = "No desc!";

            // Preparing commands for use 
            Command.RegisterCommands<MainCommands>();
            Command.RegisterCommands<ConsoleCommands>();
            Command.RegisterCommands<MathCommands>();

            Console.WriteLine("Enter \"!help\" to get a list of commands\n");

            while (true)
            {
                Console.WriteLine("Enter a command:");
                string message = Console.ReadLine();

                Console.WriteLine();
                Command.Execute(message); // Parses the message to a command and executes it
                Console.WriteLine();
            }
        }

        public static void WriteHorizontalLine(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
