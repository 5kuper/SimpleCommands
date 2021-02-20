using System;

namespace SCS.System
{
    public static class AdvancedConsole // Edit this class if you are not using System.Console in your program
    {
        /// <summary>Used for the Warn method.</summary>
        public enum WarningType { WrongCommand, WrongArguments }

        /// <summary>The default is Console.Title field.</summary>
        public static string Title
        {
            get => Console.Title;
            set => Console.Title = value;
        }

        /// <summary>The default is Console.ForegroundColor field.</summary>
        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        /// <summary>The default is Console.BackgroundColor field.</summary>
        public static ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        /// <summary>The default is Console.ReadLine method.</summary>
        public static Func<string> ReadLine => Console.ReadLine;

        /// <summary>The default is Console.Write method.</summary>
        public static Action<object> Write => Console.Write;

        public static void WriteLine(object value = null)
        {
            if (value != null)
            {
                Write(value);
            }
            Write("\n");
        }

        public static void ColoredWrite(params ColoredString[] coloredStrings)
        {
            foreach (var coloredString in coloredStrings)
            {
                ConsoleColor previousForegroundColor = ForegroundColor;
                ConsoleColor previousBackgroundColor = BackgroundColor;

                ForegroundColor = coloredString.ForegroundColor;
                BackgroundColor = coloredString.BackgroundColor;

                Write(coloredString.Value);

                ForegroundColor = previousForegroundColor;
                BackgroundColor = previousBackgroundColor;
            }
        }

        public static void ColoredWrite(ConsoleColor foregroundColor, string value)
        {
            ColoredWrite(new ColoredString(foregroundColor, value));
        }

        public static void ColoredWrite(string value, ConsoleColor backgroundColor)
        {
            ColoredWrite(new ColoredString(value, backgroundColor));
        }

        public static void ColoredWrite(ConsoleColor foregroundColor, string value, ConsoleColor backgroundColor)
        {
            ColoredWrite(new ColoredString(foregroundColor, value, backgroundColor));
        }

        public static void ColoredWriteLine(params ColoredString[] coloredStrings)
        {
            ColoredWrite(coloredStrings);
            WriteLine();
        }

        public static void ColoredWriteLine(ConsoleColor foregroundColor, string value)
        {
            ColoredWriteLine(new ColoredString(foregroundColor, value));
        }

        public static void ColoredWriteLine(string value, ConsoleColor backgroundColor)
        {
            ColoredWriteLine(new ColoredString(value, backgroundColor));
        }

        public static void ColoredWriteLine(ConsoleColor foregroundColor, string value, ConsoleColor backgroundColor)
        {
            ColoredWriteLine(new ColoredString(foregroundColor, value, backgroundColor));
        }

        /// <summary>Writes line with red color</summary>
        public static void Warn(string text)
        {
            ColoredWriteLine(ConsoleColor.Red, text);
        }

        public static void Warn(WarningType type)
        {
            switch (type)
            {
                case WarningType.WrongCommand:
                    ColoredWriteLine(ConsoleColor.Red, "Wrong command!");
                    break;
                case WarningType.WrongArguments:
                    ColoredWriteLine(ConsoleColor.Red, "Wrong arguments!");
                    break;
                default:
                    ColoredWriteLine(ConsoleColor.Red, "Error!");
                    break;
            }
        }

        public static void WriteHorizontalLine(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Write("-");
            }
            WriteLine();
        }
    }
}
