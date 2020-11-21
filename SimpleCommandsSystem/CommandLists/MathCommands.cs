using System;
using System.Collections.Generic;
using System.Data;
using static SimpleCommandsSystem.Command;

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
                Command.WriteText($"Result: {result}");
            }
            catch
            {
                Command.WriteWarning(WarningType.Other, "Wrong expression!");
            }
        }

        [Command("m!", "random", null), Command("m!", "rnd", null)]
        public static void RandomCommand()
        {
            Random random = new Random();
            Command.WriteText($"Result: {random.Next()}");
        }

        [Command("m!", "random", null), Command("m!", "rnd", null)]
        public static void RandomCommand(int minValue, int maxValue)
        {
            try
            {
                Random random = new Random();
                Command.WriteText($"Result: {random.Next(minValue, maxValue)}");
            }
            catch (Exception e)
            {
                if (e is System.ArgumentOutOfRangeException)
                {
                    Command.WriteWarning(otherWarningText: "Wrong arguments! minValue cannot be greater than maxValue!");
                }
                else
                {
                    Command.WriteWarning(WarningType.WrongArguments);
                }
            }
        }
    }
}
