using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SimpleCommandsSystem
{
    public class Command
    {
        private static string _standardPrefix = "/";
        /// <summary>It will be sets for a command if it's prefix is <see langword="null"/> in the command attribute. The default is "/".</summary>
        public static string StandardPrefix 
        {
            get
            {
                return _standardPrefix;
            }
            set
            {
                _standardPrefix = value ?? String.Empty;
            }
        }

        private static string _standardDescription = "No description";
        /// <summary>It will be sets for a command if it's description is <see langword="null"/> in the command attribute. The default is "No description".</summary>
        public static string StandardDescription
        {
            get
            {
                return _standardDescription;
            }
            set
            {
                _standardDescription = value ?? String.Empty;
            }
        }

        /// <summary>The list of all used prefixes in commands from registered classes.</summary>
        public static List<string> Prefixes { get; private set; } = new List<string>();

        /// <summary>The list of all commands from registered classes.</summary>
        public static List<Command> Commands { get; private set; } = new List<Command>();

        private string _prefix;
        public string Prefix
        {
            get
            {
                return _prefix;
            }
            private set
            {
                _prefix = value;
                if (!Prefixes.Contains(value))
                {
                    Prefixes.Add(value);
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value.Replace(" ", ""); ;
            }
        }

        public string Description { get; private set; } 
        public string[] Tags { get; private set; } // It will be used for filter search and something else, maybe...

        public readonly MethodInfo Method;
        public readonly MemberInfo Class;

        public readonly ParameterInfo[] Parameters;
        public readonly bool ContainsParameters;

        public Command(MethodInfo method, CommandAttribute attribute)
        {
            Method = method;
            Class = method.DeclaringType;

            Parameters = method.GetParameters();
            ContainsParameters = Parameters.Length > 0;

            Prefix = attribute.Prefix;
            Name = attribute.Name;
            Description = attribute.Description;
            Tags = attribute.Tags;
        }

        /// <summary>Prepares commands from the class for use.</summary>
        /// <typeparam name="ClassWithCommands">The class with commands.</typeparam>
        public static void RegisterCommands<ClassWithCommands>()
        {
            Type type = typeof(ClassWithCommands);
            MethodInfo[] methods = type.GetMethods();

            foreach (MethodInfo method in methods)
            {
                if (method.IsStatic)
                {
                    var attrebutes = method.GetCustomAttributes(false);

                    foreach (var attrebute in attrebutes)
                    {
                        if (attrebute is CommandAttribute commandAttribute)
                        {
                            Commands.Add(new Command(method, commandAttribute));
                        }
                    }
                }
            }
        }

        /// <summary>Finds and returns a list of commands with the specified name.</summary>
        public static List<Command> Find(string name) 
        {
            // TODO: Find by prefix, description, tags, class and more

            List<Command> foundCommands = new List<Command>();
            foreach (Command command in Commands)
            {
                if (command.Name == name)
                {
                    foundCommands.Add(command);
                }
            }
            return foundCommands;
        }

        /// <summary>
        /// Parses the message to a command and executes it.
        /// If the message by arguments matches several commands, the one that was added earlier is called.
        /// </summary>
        public static void Execute(string message)
        {
            List<string> words = new List<string>();
            List<string> matchingPrefixes = new List<string>();

            string commandPrefix;
            string commandName;
            List<object> arguments = new List<object>();

            List<Command> matchingCommands = new List<Command>();
            List<Command> unmatchingCommands = new List<Command>();

            #region Finding a prefix
            foreach (string prefix in Prefixes)
            {
                if (message.IndexOf(prefix) == 0)
                {
                    matchingPrefixes.Add(prefix);
                }
            }

            if (matchingPrefixes.Count == 0)
            {
                commandPrefix = String.Empty;
            }
            else
            {
                commandPrefix = matchingPrefixes.OrderByDescending(s => s.Length).First();
            }
            #endregion

            #region Finding matching commands
            message = message.Substring(commandPrefix.Length);

            // TODO: Don't split words if it is in quotes
            words.AddRange(message.Split(' '));

            commandName = words[0];
            matchingCommands = Find(commandName);

            words.Remove(commandName);
            foreach (string word in words)
            {
                arguments.Add(word);
            }

            foreach (Command command in matchingCommands)
            {
                ParameterInfo[] parametersInfo = command.Method.GetParameters();
                if (command.Prefix != commandPrefix || parametersInfo.Length != arguments.Count)
                {
                    unmatchingCommands.Add(command);
                }
            }

            foreach (Command command in unmatchingCommands)
            {
                matchingCommands.Remove(command);
            }

            if (matchingCommands.Count == 0)
            {
                Text.Warn(Text.WarningType.WrongCommand);
                return;
            }
            #endregion

            #region Finding parameters and executing the most matching command
            foreach (Command command in matchingCommands)
            {
                if (command.ContainsParameters)
                {
                    ParameterInfo[] parametersInfo = command.Method.GetParameters();

                    // TODO: Parse for commands with the last parameter is an array

                    try
                    {
                        for (int i = 0; i < parametersInfo.Length; i++)
                        {
                            Type type = parametersInfo[i].ParameterType;
                            arguments[i] = Convert.ChangeType(arguments[i], type);
                        }
                    }
                    catch
                    {
                        if (command == matchingCommands.Last())
                        {
                            Text.Warn(Text.WarningType.WrongArguments);
                        }
                        continue;
                    }

                    command.Execute(arguments.ToArray());
                    return;
                }
                else
                {
                    command.Execute();
                    return;
                }
            }
            #endregion
        }

        /// <summary>Executes the command.</summary>
        public void Execute(params object[] parameters)
        {
            try
            {
                Method.Invoke(null, parameters);
            }
            catch
            {
                Text.Warn(Text.WarningType.WrongArguments);
            }
        }
    }

    /// <summary>
    /// Creates a command based on the method. The method must be <see langword="public"/> and <see langword="static"/>.
    /// Use <see langword="null"/> in the prefix/description to set the standard value. 
    /// Don't use spaces for the command name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {
        public readonly string Prefix;
        public readonly string Name;
        public readonly string Description;
        public readonly string[] Tags;

        public CommandAttribute(string prefix, string name, string description, params string[] tags)
        {
            Prefix = prefix ?? Command.StandardPrefix;
            Name = name;
            Description = description ?? Command.StandardDescription;
            Tags = tags ?? new string[0];
        }
    }
}
