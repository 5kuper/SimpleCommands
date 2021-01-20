using System;
using System.Threading;
using SCS.System;

namespace SCS.Commands
{
    class ConsoleCommands // Change or don't use this class if you are not using System.Console in your program
    {
        public const string Prefix = "c!";
        public static readonly string DefaultTitle = Console.Title;

        private ConsoleCommands() { }

        [Command(Prefix, "help", "List of console commands.", "Help")]
        public static void HelpCommand()
        {
            MainCommands.HelpCommand(Prefix);
        }

        [Command(Prefix, "beep", "Makes a beep")]
        public static void BeepCommand(int quantity = 1)
        {
            if (quantity == 1)
            {
                Text.Write("Beep!");
                Console.Beep();
            }
            else if (quantity > 1)
            {
                Text.Write("B", false);
                for (int i = 0; i < quantity; i++)
                {
                    Thread.Sleep(100);
                    Text.Write("ee", false);
                    Console.Beep();
                }
                Text.Write("p!\n", false);
            }
            else
            {
                Text.Write("Not beep!");
            }
        }

        [Command(Prefix, "change-title", "Sets the title of the console.")]
        public static void ChangeTitleCommand(string title)
        {
            Console.Title = title;
            Text.Write($"The title changed to \"{title}\"");
        }

        [Command(Prefix, "reset-title", "Sets the title of the console to the default value.")]
        public static void ResetTitleCommand()
        {
            Console.Title = DefaultTitle;
            Text.Write($"The title is reset to the default value");
        }

        [Command(Prefix, "change-foreground-color", "Sets the foreground color of the console.")]
        public static void ChangeForegroundColorCommand(string colorName)
        {
            try
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Text.Write($"Foreground color changed to {Console.ForegroundColor}");
            }
            catch
            {
                Text.Warn(otherWarningText: "Wrong color name!");
            }
        }

        [Command(Prefix, "change-background-color", "Sets the background color of the console.")]
        public static void ChangeBackgroundColorCommand(string colorName)
        {
            try
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Text.Write($"Background color changed to {Console.BackgroundColor}");
            }
            catch
            {
                Text.Warn(otherWarningText: "Wrong color name!"); 
            }
        }

        [Command(Prefix, "reset-color", "Sets the foreground and background console colors to their defaults.")]
        public static void ResetColorCommand()
        {
            Console.ResetColor();
            Text.Write("Foreground and background console colors are reset to their default values"); 
        }

        [Command(Prefix, "clear", "Clears the console.")]
        public static void ClearCommand()
        {
            Console.Clear();
            Text.Write("The console cleared");
        }
    }
}
