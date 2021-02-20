using System;

namespace SCS.System
{
    public class ColoredString
    {
        public string Value { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }

        public ColoredString(ConsoleColor foregroundColor, string value, ConsoleColor backgroundColor)
        {
            Value = value;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public ColoredString(ConsoleColor foregroundColor, string value) : this(foregroundColor, value, AdvancedConsole.BackgroundColor) { }

        public ColoredString(string value, ConsoleColor backgroundColor) : this(AdvancedConsole.ForegroundColor, value, backgroundColor) { }

        public ColoredString(string value) : this(AdvancedConsole.ForegroundColor, value, AdvancedConsole.BackgroundColor) { }
    }
}
