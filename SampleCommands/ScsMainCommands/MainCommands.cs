using System;
using System.Reflection;
using SCS.System;
using System.Collections.Generic;
using System.Linq;
using ListScanner = SCS.System.ListCommandScanner;
using CStr = SCS.System.ColoredString;

namespace SCS.Commands
{
    public class MainCommands
    {
        public const string Prefix = null;

        private MainCommands() { }

        [Command(Prefix, "help", "Main help command. Specify a prefix as argument to search by prefix.", "Help")]
        public static void HelpCommand(string prefix = null)
        {
            List<Command> commands;
            ListScanner ignoreInHelpFilter = new ListScanner("Help Ignore", ListScanner.TargetOfScanner.Tags, ListScanner.ScannerCondition.NotContains);

            if (prefix != null && !Command.Prefixes.Contains(prefix))
            {
                if (Command.Prefixes.Count <= 1)
                {
                    AdvancedConsole.Warn(AdvancedConsole.WarningType.WrongArguments);
                }
                else
                {
                    AdvancedConsole.Warn("Invalid prefix!");
                }
                return;
            }

            if (prefix == null)
            {
                #region Colored text
                AdvancedConsole.ColoredWriteLine(new CStr("Command to enter:\n"), new CStr(ConsoleColor.DarkCyan, "prefix "),
                    new CStr(ConsoleColor.Cyan, "name "), new CStr(ConsoleColor.Magenta, "argument1 argument2 argumentN\n"));

                AdvancedConsole.ColoredWriteLine(new CStr("Command in help:\n"),
                    new CStr(ConsoleColor.DarkCyan, "prefix "), new CStr(ConsoleColor.Cyan, "name "),
                    new CStr(ConsoleColor.DarkYellow, "parameter-type1 "), new CStr(ConsoleColor.Yellow, "parameter-name1"), new CStr(", "),
                    new CStr(ConsoleColor.DarkYellow, "parameter-type2 "), new CStr(ConsoleColor.Yellow, "parameter-name2 "),
                    new ColoredString("= "), new CStr(ConsoleColor.Magenta, "default-value "), new CStr(ConsoleColor.DarkGray, "| Description.\n"));
                #endregion
            }

            if (Command.Prefixes.Count == 1)
            {
                prefix = Command.Prefixes[0];
            }

            if (prefix == null)
            {
                commands = Command.Find(ignoreInHelpFilter, new ListScanner("Help"));
            }
            else
            {
                ListScanner ignoreHelpCommandsFilter = new ListScanner("Help", ListScanner.TargetOfScanner.Tags, ListScanner.ScannerCondition.NotContains);
                SimpleCommandScanner prefixFilter = new SimpleCommandScanner(prefix, SimpleCommandScanner.TargetOfScanner.Prefix);

                commands = Command.Find(ignoreInHelpFilter, ignoreHelpCommandsFilter, prefixFilter);
            }

            AdvancedConsole.WriteLine(commands.Count > 0 ? "Commands:" : "Sorry, commands not found :(");
            foreach (Command command in commands)
            {
                AdvancedConsole.ColoredWrite(ConsoleColor.DarkCyan, command.Prefix);
                AdvancedConsole.ColoredWrite(ConsoleColor.Cyan, command.Name);

                foreach (ParameterInfo parameter in command.Parameters)
                {
                    AdvancedConsole.ColoredWrite(ConsoleColor.DarkYellow, " " + parameter.ParameterType.Name + " ");
                    AdvancedConsole.ColoredWrite(ConsoleColor.Yellow, parameter.Name);

                    if (parameter.HasDefaultValue)
                    {
                        AdvancedConsole.Write(" = ");
                        AdvancedConsole.ColoredWrite(ConsoleColor.Magenta, $"{parameter.DefaultValue ?? "null"}");
                    }
                    if (parameter != command.Parameters.Last())
                    {
                        AdvancedConsole.Write(",");
                    }
                }

                AdvancedConsole.ColoredWriteLine(ConsoleColor.DarkGray, $" | {command.Description}");
            }
        }
    }
}
