using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleCommandsSystem.CommandLists
{
    class ConsoleCommands // Change or don't use this class if you are not using System.Console in your program
    {
        private ConsoleCommands() { }

        [Command("c!", "beep", null)]
        public static void BeepCommand()
        {
            Command.WriteText("Beep!");
            Console.Beep();
        }

        [Command("c!", "beep", null)]
        public static void BeepCommand(int quantity)
        {
            if (quantity > 0)
            {
                Command.WriteText("B", false);
                for (int i = 0; i < quantity; i++)
                {
                    Thread.Sleep(100);
                    Command.WriteText("e", false);
                    Console.Beep();
                }
                Command.WriteText("p!\n", false);
            }
            else
            {
                Command.WriteText("Not beep!");
            }
        }

        [Command("c!", "change-foreground-color", null)]
        public static void ChangeForegroundColorCommand(string colorName)
        {
            try
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Command.WriteText($"Foreground color changed to {Console.ForegroundColor}");
            }
            catch
            {
                Command.WriteWarning(otherWarningText: "Wrong color name!");
            }
        }

        [Command("c!", "change-background-color", null)]
        public static void ChangeBackgroundColorCommand(string colorName)
        {
            try
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Command.WriteText($"Background color changed to {Console.BackgroundColor}");
            }
            catch
            {
                Command.WriteWarning(otherWarningText: "Wrong color name!");
            }
        }
    }
}
