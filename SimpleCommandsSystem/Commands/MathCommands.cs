using System;
using System.Data;
using SCS.System;
using static SCS.System.Text;

namespace SCS.Commands
{
    class MathCommands
    {
        public const string Prefix = "m!";

        private MathCommands() { }

        [Command(Prefix, "help", "List of math commands.", "Help")]
        public static void HelpCommand()
        {
            MainCommands.HelpCommand(Prefix);
        }

        [Command(Prefix, "compute", null)]
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

        [Command(Prefix, "random", null), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand()
        {
            Random random = new Random();
            Text.Write($"Result: {random.Next()}");
        }

        [Command(Prefix, "random", null), Command("m!", "rnd", null, "Help Ignore")]
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
