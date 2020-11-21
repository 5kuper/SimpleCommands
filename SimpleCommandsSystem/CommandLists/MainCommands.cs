using System;
using System.Reflection;

namespace SimpleCommandsSystem.CommandLists
{
    class MainCommands
    {
        private MainCommands() { }

        [Command(null, "help", null)]
        public static void HelpCommand()
        {
            Command.WriteText("All commands:");
            foreach (Command command in Command.Commands)
            {
                string parametersInfo = String.Empty;
                foreach (ParameterInfo parameter in command.Parameters)
                {
                    parametersInfo += $" {parameter.ParameterType.Name} {parameter.Name}";
                }
                Command.WriteText($"{command.Prefix}{command.Name}{parametersInfo} - {command.Description}");
            }
        }

        public static void WriteHorizontalLine(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Command.WriteText("-", false);
            }
            Command.WriteText();
        }
    }
}
