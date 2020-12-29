using System;
using System.Threading;
using SCS.System;

namespace SCS.Commands
{
    class ConsoleCommands // Change or don't use this class if you are not using System.Console in your program
    {
        public const string Prefix = "c!";

        private ConsoleCommands() { }

        [Command(Prefix, "help", "List of console commands.", "Help")]
        public static void HelpCommand()
        {
            MainCommands.HelpCommand(Prefix);
        }

        [Command(Prefix, "beep", null)]
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
                    Text.Write("e", false);
                    Console.Beep();
                }
                Text.Write("p!\n", false);
            }
            else
            {
                Text.Write("Not beep!");
            }
        }

        [Command(Prefix, "change-foreground-color", null)]
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

        [Command(Prefix, "change-background-color", null)]
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
    }
}
