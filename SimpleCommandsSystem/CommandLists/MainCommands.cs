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
            Console.WriteLine("All commands:");
            foreach (Command command in Command.Commands)
            {
                string parametersInfo = String.Empty;
                foreach (ParameterInfo parameter in command.Parameters)
                {
                    parametersInfo += $" {parameter.ParameterType.Name} {parameter.Name}";
                }
                Console.WriteLine($"{command.Prefix}{command.Name}{parametersInfo} - {command.Description}");
            }
        }
    }
}
