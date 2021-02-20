using System;
using SCS.System;
using SCS.Commands;

namespace SCS // SCS - Simple Commands System
{
    internal class Program
    {
        private static void Main()
        {
            AdvancedConsole.Title = "Simple Commands System";

            // If you are not using System.Console in your program,
            // Edit the AdvancedConsole class and edit or don't use the ConsoleCommands class

            // Setting standard values
            Command.StandardPrefix = "!";
            Command.StandardDescription = "No desc.";

            // Preparing commands for use 
            Command.RegisterCommands<MainCommands>(); // 1 variant 
            Command.RegisterCommands<ConsoleCommands>();
            Command.RegisterCommands(typeof(MathCommands), typeof(FileCommands)); // 2 variant 

            // Writing "Enter !help to get a list of commands" with colored "!help"
            AdvancedConsole.ColoredWriteLine(new ColoredString("Enter "), 
                new ColoredString(ConsoleColor.DarkCyan, "!"), new ColoredString(ConsoleColor.Cyan, "help "),
                new ColoredString("to get a list of commands. Don't use commas between arguments and use string arguments in quotes.\n"));

            while (true)
            {
                AdvancedConsole.Write("Enter a command: ");
                string message = AdvancedConsole.ReadLine();

                AdvancedConsole.WriteLine();
                Command.Execute(message); // Parses the message to a command and executes it
                AdvancedConsole.WriteLine();
            }
        }
    }
}
