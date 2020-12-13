using System;
using System.Collections.Generic;
using System.Data;
using SCS.System;
using SCS.Commands;
using static SCS.System.Text;

namespace SCS.Commands
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
                Text.Write($"Result: {result}");
            }
            catch
            {
                Text.Warn(WarningType.Other, "Wrong expression!");
            }
        }

        [Command("m!", "random", null), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand()
        {
            Random random = new Random();
            Text.Write($"Result: {random.Next()}");
        }

        [Command("m!", "random", null), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand(int minValue, int maxValue)
        {
            try
            {
                Random random = new Random();
                Text.Write($"Result: {random.Next(minValue, maxValue)}");
            }
            catch (Exception e)
            {
                if (e is ArgumentOutOfRangeException)
                {
                    Text.Warn(otherWarningText: "Wrong arguments! minValue cannot be greater than maxValue!");
                }
                else
                {
                    Text.Warn(WarningType.WrongArguments);
                }
            }
        }
    }
}
