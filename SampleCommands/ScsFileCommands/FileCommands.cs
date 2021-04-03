using System;
using System.IO;
using SCS.System;

namespace SCS.Commands
{
    public class FileCommands
    {
        public const string Prefix = "f!";

        private FileCommands() { }

        [Command(Prefix, "help", "List of file commands.", "Help")]
        public static void HelpCommand() => MainCommands.HelpCommand(Prefix);

        [Command(Prefix, "read", "Reads the text in the file.")]
        public static void ReadCommand(string path)
        {
            try
            {
                AdvancedConsole.WriteLine(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                if (e is DirectoryNotFoundException)
                {
                    AdvancedConsole.Warn("Directory Not Found!");
                }
                else if (e is FileNotFoundException)
                {
                    AdvancedConsole.Warn("File Not Found!");
                }
                else if (e is PathTooLongException)
                {
                    AdvancedConsole.Warn("Path Too Long!");
                }
                else if (e is UnauthorizedAccessException)
                {
                    AdvancedConsole.Warn("Unauthorized Access!");
                }
                else
                {
                    AdvancedConsole.Warn(AdvancedConsole.WarningType.WrongArguments);
                }
            }
        }

        [Command(Prefix, "write", "Writes the string to the file.")]
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
                    AdvancedConsole.Warn("Directory Not Found!");
                }
                else if (e is PathTooLongException)
                {
                    AdvancedConsole.Warn("Path Too Long!");
                }
                else if (e is UnauthorizedAccessException)
                {
                    AdvancedConsole.Warn("Unauthorized Access!");
                }
                else
                {
                    AdvancedConsole.Warn(AdvancedConsole.WarningType.WrongArguments);
                }
                return;
            }
            AdvancedConsole.WriteLine("Text successfully written!");
        }
    }
}
