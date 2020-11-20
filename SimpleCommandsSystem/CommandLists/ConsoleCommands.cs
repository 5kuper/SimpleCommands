using System;
using System.Collections.Generic;
using System.Threading;

namespace SimpleCommandsSystem.CommandLists
{
    class ConsoleCommands
    {
        private ConsoleCommands() { }

        [Command("c!", "beep", null)]
        public static void BeepCommand()
        {
            Console.WriteLine("Beep!");
            Console.Beep();
        }

        [Command("c!", "beep", null)]
        public static void BeepCommand(int quantity)
        {
            if (quantity > 0)
            {
                Console.Write("B");
                for (int i = 0; i < quantity; i++)
                {
                    Thread.Sleep(100);
                    Console.Write("e");
                    Console.Beep();
                }
                Console.Write("p!\n");
            }
            else
            {
                Console.WriteLine("Not beep!");
            }
        }

        [Command("c!", "change-foreground-color", null)]
        public static void ChangeForegroundColorCommand(string colorName)
        {
            try
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Console.WriteLine($"Foreground color changed to {Console.ForegroundColor}");
            }
            catch
            {
                Console.WriteLine("Wrong color name!");
            }
        }

        [Command("c!", "change-background-color", null)]
        public static void ChangeBackgroundColorCommand(string colorName)
        {
            try
            {
                Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                Console.WriteLine($"Background color changed to {Console.BackgroundColor}");
            }
            catch
            {
                Console.WriteLine("Wrong color name!");
            }
        }
    }
}
