using System;
using System.Threading;
using SCS.System;

namespace SCS.Commands
{
    class ConsoleCommands // Edit or don't use this class if you are not using System.Console in your program
    {
        public const string Prefix = "c!";
        public static readonly string DefaultTitle = AdvancedConsole.Title;

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
                AdvancedConsole.WriteLine("Beep!");
                Console.Beep();
            }
            else if (quantity > 1)
            {
                AdvancedConsole.Write("B");
                for (int i = 0; i < quantity; i++)
                {
                    Thread.Sleep(100);
                    AdvancedConsole.Write("ee");
                    Console.Beep();
                }
                AdvancedConsole.Write("p!\n");
            }
            else
            {
                AdvancedConsole.WriteLine("Not beep!");
            }
        }

        [Command(Prefix, "change-title", "Sets the title of the console.")]
        public static void ChangeTitleCommand(string title)
        {
            AdvancedConsole.Title = title;
            AdvancedConsole.WriteLine($"The title changed to \"{title}\"");
        }

        [Command(Prefix, "reset-title", "Sets the title of the console to the default value.")]
        public static void ResetTitleCommand()
        {
            AdvancedConsole.Title = DefaultTitle;
            AdvancedConsole.WriteLine($"The title is reset to the default value");
        }

        [Command(Prefix, "change-foreground-color", "Sets the foreground color of the console.")]
        public static void ChangeForegroundColorCommand(string colorName)
        {
            try
            {
                AdvancedConsole.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                AdvancedConsole.WriteLine($"Foreground color changed to {AdvancedConsole.ForegroundColor}");
            }
            catch
            {
                AdvancedConsole.Warn("Wrong color name!");
            }
        }

        [Command(Prefix, "change-background-color", "Sets the background color of the console.")]
        public static void ChangeBackgroundColorCommand(string colorName)
        {
            try
            {
                AdvancedConsole.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                AdvancedConsole.WriteLine($"Background color changed to {AdvancedConsole.BackgroundColor}");
            }
            catch
            {
                AdvancedConsole.Warn("Wrong color name!"); 
            }
        }

        [Command(Prefix, "reset-color", "Sets the foreground and background console colors to their defaults.")]
        public static void ResetColorCommand()
        {
            Console.ResetColor();
            AdvancedConsole.WriteLine("Foreground and background console colors are reset to their default values"); 
        }

        [Command(Prefix, "clear", "Clears the console.")]
        public static void ClearCommand()
        {
            Console.Clear();
            AdvancedConsole.WriteLine("The console cleared");
        }
    }
}
