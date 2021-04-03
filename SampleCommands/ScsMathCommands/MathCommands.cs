using System;
using System.Data;
using SCS.System;

namespace SCS.Commands
{
    public class MathCommands
    {
        public const string Prefix = "m!";

        private MathCommands() { }

        [Command(Prefix, "help", "List of math commands.", "Help")]
        public static void HelpCommand() => MainCommands.HelpCommand(Prefix);

        [Command(Prefix, "compute", "Computes the given expression.")]
        public static void ComputeCommand(string expression)
        {
            try
            {
                using DataTable table = new DataTable();
                object result = table.Compute(expression, string.Empty);
                AdvancedConsole.WriteLine($"Result: {result}");
            }
            catch
            {
                AdvancedConsole.Warn("Wrong expression!");
            }
        }

        [Command(Prefix, "random", "Returns a non-negative random integer. Alternative name is rnd."), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand()
        {
            Random random = new Random();
            AdvancedConsole.WriteLine($"Result: {random.Next()}");
        }

        [Command(Prefix, "random", "Returns a non-negative random integer that is less than the specified maximum."), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand(int maxValue)
        {
            Random random = new Random();
            AdvancedConsole.WriteLine($"Result: {random.Next(maxValue)}");
        }

        [Command(Prefix, "random", "Returns a random integer that is within a specified range."), Command("m!", "rnd", null, "Help Ignore")]
        public static void RandomCommand(int minValue, int maxValue)
        {
            try
            {
                Random random = new Random();
                AdvancedConsole.WriteLine($"Result: {random.Next(minValue, maxValue)}");
            }
            catch (Exception e)
            {
                if (e is ArgumentOutOfRangeException)
                {
                    AdvancedConsole.Warn("Wrong arguments! minValue cannot be greater than maxValue!");
                }
                else
                {
                    AdvancedConsole.Warn(AdvancedConsole.WarningType.WrongArguments);
                }
            }
        }

        [Command(Prefix, "pow", "Returns a specified number raised to the specified power.")]
        public static void PowCommand(double a, double b) => AdvancedConsole.WriteLine($"Result: {Math.Pow(a, b)}");

        [Command(Prefix, "sqrt", "Returns the square root of a specified number.")]
        public static void SqrtCommand(double a) => AdvancedConsole.WriteLine($"Result: {Math.Sqrt(a)}");

        [Command(Prefix, "sin", "Returns the sine of the specified angle.")]
        public static void SinCommand(double a) => AdvancedConsole.WriteLine($"Result: {Math.Sin(a)}");

        [Command(Prefix, "cos", "Returns the cosine of the specified angle.")]
        public static void CosCommand(double a) => AdvancedConsole.WriteLine($"Result: {Math.Cos(a)}");

        [Command(Prefix, "tan", "Returns the tangent of the specified angle.")]
        public static void TanCommand(double a) => AdvancedConsole.WriteLine($"Result: {Math.Tan(a)}");

        [Command(Prefix, "round", "Rounds a value to the nearest integral value.")]
        public static void RoundCommand(decimal a) => AdvancedConsole.WriteLine($"Result: {Math.Round(a)}");

        [Command(Prefix, "round", "Rounds a decimal value to a specified number of fractional digits.")]
        public static void RoundCommand(decimal a, int b) => AdvancedConsole.WriteLine($"Result: {Math.Round(a, b)}");
    }
}
