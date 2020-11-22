using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCommandsSystem
{
    static class Text
    {
        /// <summary>Used for the Warn method.</summary>
        public enum WarningType { WrongCommand, WrongArguments, Other }

        public delegate string ReadDelegate();
        public delegate void WriteDelegate(string text = "", bool makeNewLine = true, string foregroundColorName = null, string backgroundColorName = null);
        public delegate void WarnDelegate(WarningType warningType = WarningType.Other, string otherWarningText = null);

        /// <summary>
        /// <see langword="delegate"/> <see langword="void"/> Text.ReadDelegate()
        /// <para>The default is Console.ReadLine method.</para>
        /// <para>Change it if you are not using System.Console in your program.</para>
        /// </summary>
        public static ReadDelegate Read { get; set; } = Console.ReadLine;

        /// <summary>
        /// <see langword="delegate"/> <see langword="void"/> Text.WriteDelegate(string text = "", bool makeNewLine = true, string foregroundColorName = null, string backgroundColorName = null)
        /// <para>The default is WriteInSystemConsole method, which uses the Console.WriteLine and Console.Write methods.</para>
        /// <para>Change it if you are not using System.Console in your program.</para>
        /// <para>To prevent exception throw, use nameof(ConsoleColor.Color) in the foregroundColorName and backgroundColorName.</para>
        /// </summary>
        public static WriteDelegate Write { get; set; } = WriteInSystemConsole;

        /// <summary>
        /// <see langword="delegate"/> <see langword="void"/> Text.WarnDelegate(WarningType warningType = WarningType.Other, string otherWarningText = null)
        /// <para>The default is WarnInSystemConsole method, which uses the Text.Write method.</para>
        /// <para>If set to the default, use the otherWarningText only if the warningType is WarningType.Other.</para>
        /// </summary>
        public static WarnDelegate Warn { get; set; } = WarnInSystemConsole;

        public static void WriteInSystemConsole(string text = "", bool makeNewLine = true, string foregroundColorName = null, string backgroundColorName = null)
        {
            ConsoleColor previousForegroundColor = Console.ForegroundColor;
            ConsoleColor previousBackgroundColor = Console.BackgroundColor;

            try
            {
                if (foregroundColorName != null)
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), foregroundColorName, true);
                }
                if (backgroundColorName != null)
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), backgroundColorName, true);
                }
            }
            catch
            {
                throw new Exception("Wrong ConsoleColor name! To prevent exception throw, use nameof(ConsoleColor.Color) in the foregroundColorName and backgroundColorName.");
            }
            finally
            {
                if (makeNewLine)
                {
                    Console.WriteLine(text);
                }
                else
                {
                    Console.Write(text);
                }

                Console.ForegroundColor = previousForegroundColor;
                Console.BackgroundColor = previousBackgroundColor;
            }
        }

        public static void WarnInSystemConsole(WarningType warningType = WarningType.Other, string otherWarningText = null)
        {
            switch (warningType)
            {
                case WarningType.WrongCommand:
                    Write("Wrong command!", foregroundColorName: "Red");
                    break;
                case WarningType.WrongArguments:
                    Write("Wrong arguments!", foregroundColorName: "Red");
                    break;
                case WarningType.Other:
                    if (otherWarningText != null)
                    {
                        Write(otherWarningText, foregroundColorName: "Red");
                    }
                    else
                    {
                        Write("Error!", foregroundColorName: "Red");
                    }
                    break;
            }
        }

        public static void WriteHorizontalLine(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Text.Write("-", false);
            }
            Text.Write();
        }
    }
}
