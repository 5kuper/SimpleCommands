using System;
using System.Reflection;
using SCS.System;
using SCS.Commands;
using System.Collections.Generic;
using ListScanner = SCS.System.ListCommandScanner;

namespace SCS.Commands
{
    class MainCommands
    {
        private MainCommands() { }

        [Command(null, "help", null, "Help Ignore")]
        public static void HelpCommand()
        {
            List<Command> commands = Command.Find(new ListScanner("Help Ignore", ListScanner.TargetOfScanner.Tags, ListScanner.ScannerCondition.NotContains));

            Text.Write("Commands:");
            foreach (Command command in commands)
            {
                string parametersInfo = String.Empty;
                foreach (ParameterInfo parameter in command.Parameters)
                {
                    parametersInfo += $" {parameter.ParameterType.Name} {parameter.Name}";
                }
                Text.Write($"{command.Prefix}{command.Name}{parametersInfo} - {command.Description}");
            }
        }
    }
}
