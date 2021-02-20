using System;

namespace SCS.System
{
    public class ColoredString
    {
        public string Value { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public ColoredString(string value)
        {
            Value = value;
            ForegroundColor = AdvancedConsole.ForegroundColor;
            BackgroundColor = AdvancedConsole.BackgroundColor;
        }

        public ColoredString(ConsoleColor foregroundColor, string value)
        {
            Value = value;
            ForegroundColor = foregroundColor;
            BackgroundColor = AdvancedConsole.BackgroundColor;
        }

        public ColoredString(string value, ConsoleColor backgroundColor)
        {
            Value = value;
            ForegroundColor = AdvancedConsole.ForegroundColor;
            BackgroundColor = backgroundColor;
        }

        public ColoredString(ConsoleColor foregroundColor, string value, ConsoleColor backgroundColor)
        {
            Value = value;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}
