using System;
using System.Collections.Generic;
using System.IO;
using static SimpleCommandsSystem.Text;

namespace SimpleCommandsSystem.CommandLists
{
    class FileCommands
    {
        private FileCommands() { }

        [Command("f!", "read", null)]
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

        [Command("f!", "write", null)]
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
