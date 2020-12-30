using System;
using System.Reflection;
using SCS.System;
using System.Collections.Generic;
using System.Linq;
using ListScanner = SCS.System.ListCommandScanner;

namespace SCS.Commands
{
    class MainCommands
    {
        public const string Prefix = null;

        private MainCommands() { }

        [Command(Prefix, "help", "Main help command. Specify a prefix as argument to search by prefix.", "Help")]
        public static void HelpCommand(string prefix = null)
        {
            List<Command> commands;
            ListScanner ignoreInHelpFilter = new ListScanner("Help Ignore", ListScanner.TargetOfScanner.Tags, ListScanner.ScannerCondition.NotContains);

            if (prefix == null)
            {
                #region Colored text
                // My system of colored text is bad :(

                Text.Write("Command to enter:");
                Text.Write("prefix ", false, nameof(ConsoleColor.DarkCyan));
                Text.Write("name ", false, nameof(ConsoleColor.Cyan));
                Text.Write("argument1 argument2 argumentN\n", true, nameof(ConsoleColor.Magenta));

                Text.Write("Command in help:");
                Text.Write("prefix ", false, nameof(ConsoleColor.DarkCyan));
                Text.Write("name ", false, nameof(ConsoleColor.Cyan));
                Text.Write("parameter-type1 ", false, nameof(ConsoleColor.DarkYellow));
                Text.Write("parameter-name1", false, nameof(ConsoleColor.Yellow));
                Text.Write(", ", false);
                Text.Write("parameter-type2 ", false, nameof(ConsoleColor.DarkYellow));
                Text.Write("parameter-name2 ", false, nameof(ConsoleColor.Yellow));
                Text.Write("= ", false);
                Text.Write("default-value ", false, nameof(ConsoleColor.Magenta));
                Text.Write("| Description.\n", true, nameof(ConsoleColor.DarkGray));
                #endregion

                commands = Command.Find(ignoreInHelpFilter, new ListScanner("Help"));
            }
            else
            {
                ListScanner ignoreHelpCommandsFilter = new ListScanner("Help", ListScanner.TargetOfScanner.Tags, ListScanner.ScannerCondition.NotContains);
                SimpleCommandScanner prefixFilter = new SimpleCommandScanner(prefix, SimpleCommandScanner.TargetOfScanner.Prefix);

                commands = Command.Find(ignoreInHelpFilter, ignoreHelpCommandsFilter, prefixFilter);
            }

            Text.Write(commands.Count > 0 ? "Commands:" : "Sorry, commands not found :(");
            foreach (Command command in commands)
            {
                Text.Write(command.Prefix, false, nameof(ConsoleColor.DarkCyan));
                Text.Write(command.Name, false, nameof(ConsoleColor.Cyan));

                foreach (ParameterInfo parameter in command.Parameters)
                {
                    Text.Write(" " + parameter.ParameterType.Name + " ", false, nameof(ConsoleColor.DarkYellow));
                    Text.Write(parameter.Name, false, nameof(ConsoleColor.Yellow));

                    if (parameter.HasDefaultValue)
                    {
                        Text.Write(" = ", false);
                        Text.Write($"{parameter.DefaultValue ?? "null"}", false, nameof(ConsoleColor.Magenta));
                    }
                    if (parameter != command.Parameters.Last())
                    {
                        Text.Write(",", false);
                    }
                }

                Text.Write($" | {command.Description}", true, nameof(ConsoleColor.DarkGray));
            }
        }
    }
}
