using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleCommandsSystem.CommandLists
{
    class MathCommands
    {
        private MathCommands() { }

        [Command("m!", "compute", null)]
        public static void ComputeCommand(string expression)
        {
            try
            {
                using DataTable table = new DataTable();
                object result = table.Compute(expression, string.Empty);
                Console.WriteLine($"Result: {result}");
            }
            catch
            {
                Console.WriteLine("Wrong expression!");
            }
        }

        [Command("m!", "random", null), Command("m!", "rnd", null)]
        public static void RandomCommand()
        {
            Random random = new Random();
            Console.WriteLine($"Result: {random.Next()}");
        }

        [Command("m!", "random", null), Command("m!", "rnd", null)]
        public static void RandomCommand(int minValue, int maxValue)
        {
            try
            {
                Random random = new Random();
                Console.WriteLine($"Result: {random.Next(minValue, maxValue)}");
            }
            catch (Exception e)
            {
                if (e is System.ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Wrong parameters! minValue cannot be greater than maxValue!");
                }
                else
                {
                    Console.WriteLine("Wrong parameters!");
                }
            }
        }
    }
}
