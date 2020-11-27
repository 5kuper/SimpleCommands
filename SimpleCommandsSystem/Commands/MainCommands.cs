using System;
using System.Reflection;
using SCS.System;
using SCS.Commands;

namespace SCS.Commands
{
    class MainCommands
    {
        private MainCommands() { }

        [Command(null, "help", null)]
        public static void HelpCommand()
        {
            Text.Write("All commands:");
            foreach (Command command in Command.Commands)
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
