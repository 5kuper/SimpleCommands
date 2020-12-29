using System;
using System.IO;
using SCS.System;
using static SCS.System.Text;

namespace SCS.Commands
{
    class FileCommands
    {
        public const string Prefix = "f!";

        private FileCommands() { }

        [Command(Prefix, "help", "List of file commands.", "Help")]
        public static void HelpCommand()
        {
            MainCommands.HelpCommand(Prefix);
        }

        [Command(Prefix, "read", null)]
        public static void ReadCommand(string path)
        {
            try
            {
                Text.Write(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    Text.Warn(otherWarningText: $"Directory Not Found!");
                }
                else if (e is FileNotFoundException)
                {
                    Text.Warn(otherWarningText: $"File Not Found!");
                }
                else if (e is PathTooLongException)
                {
                    Text.Warn(otherWarningText: $"Path Too Long!");
                }
                else if (e is UnauthorizedAccessException)
                {
                    Text.Warn(otherWarningText: $"Unauthorized Access!");
                }
                else
                {
                    Text.Warn(WarningType.WrongArguments);
                }
            }
        }

        [Command(Prefix, "write", null)]
        public static void WriteCommand(string path, string contents)
        {
            try
            {
                File.WriteAllText(path, contents);
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    Text.Warn(otherWarningText: $"Directory Not Found!");
                }
                else if (e is PathTooLongException)
                {
                    Text.Warn(otherWarningText: $"Path Too Long!");
                }
                else if (e is UnauthorizedAccessException)
                {
                    Text.Warn(otherWarningText: $"Unauthorized Access!");
                }
                else
                {
                    Text.Warn(WarningType.WrongArguments);
                }
                return;
            }
            Text.Write($"Text successfully written!");
        }
    }
}
