using System;
using SCS.System;
using SCS.Commands;

namespace SCS // SCS - Simple Commands System
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Simple Commands System";

            // If you are not using System.Console in your program,
            // change the Text.Read and Text.Write and change or don't use the ConsoleCommands class

            // Setting standard values
            Command.StandardPrefix = "!";
            Command.StandardDescription = "No desc!";

            // Preparing commands for use 
            Command.RegisterCommands<MainCommands>();
            Command.RegisterCommands<ConsoleCommands>();
            Command.RegisterCommands<MathCommands>();
            Command.RegisterCommands<FileCommands>();

            Text.Write("Enter ", false);
            Text.Write("!help ", false, nameof(ConsoleColor.Cyan));
            Text.Write("to get a list of commands\n");

            while (true)
            {
                Text.Write("Enter a command:");
                string message = Text.Read();

                Text.Write();
                Command.Execute(message); // Parses the message to a command and executes it
                Text.Write();
            }
        }
    }
}
