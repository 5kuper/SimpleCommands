using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using SCS.ConsoleMusic;
using SCS.System;

namespace SCS.Commands
{
    internal class ConsoleCommands // Edit or don't use this class if you are not using System.Console in your program
    {
        public const string Prefix = "c!";
        public static readonly string DefaultTitle;

        private static readonly XmlSerializer TuneSerializer = new XmlSerializer(typeof(Tune));
        private static Tune _hackingToTheGateTune;

        static ConsoleCommands() => DefaultTitle = new string(AdvancedConsole.Title);

        private ConsoleCommands() { }


        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private static readonly IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private enum ShowWindowType { Hide = 0, Maximize = 3, Minimize = 6, Restore = 9 }


        [Command(Prefix, "help", "List of console commands.", "Help")]
        public static void HelpCommand() => MainCommands.HelpCommand(Prefix);

        [Command(Prefix, "beep", "Makes a beep.")]
        public static void BeepCommand(int quantity = 1)
        {
            if (quantity == 1)
            {
                AdvancedConsole.WriteLine("Beep!");
                Console.Beep();
            }
            else if (quantity > 1)
            {
                AdvancedConsole.Write("B");
                for (int i = 0; i < quantity; i++)
                {
                    Thread.Sleep(100);
                    AdvancedConsole.Write("ee");
                    Console.Beep();
                }
                AdvancedConsole.Write("p!\n");
            }
            else
            {
                AdvancedConsole.WriteLine("Not beep!");
            }
        }

        [Command(Prefix, "beep-music", "Plays a beep music.")]
        public static void BeepMusicCommand(string pathToXml)
        {
            try
            {
                Tune tune;

                using (FileStream stream = new FileStream(pathToXml, FileMode.Open))
                {
                    tune = (Tune)TuneSerializer.Deserialize(stream);
                }

                tune.Play();
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    AdvancedConsole.Warn("Invalid File!");
                }
                else if (e is DirectoryNotFoundException)
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

        [Command(Prefix, "change-title", "Sets the title of the console.")]
        public static void ChangeTitleCommand(string title)
        {
            AdvancedConsole.Title = title;
            AdvancedConsole.WriteLine($"The title changed to \"{title}\"");
        }

        [Command(Prefix, "reset-title", "Sets the title of the console to the default value.")]
        public static void ResetTitleCommand()
        {
            AdvancedConsole.Title = DefaultTitle;
            AdvancedConsole.WriteLine("The title is reset to the default value");
        }

        [Command(Prefix, "change-foreground-color", "Sets the foreground color of the console.")]
        public static void ChangeForegroundColorCommand(string colorName)
        {
            try
            {
                AdvancedConsole.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                AdvancedConsole.WriteLine($"Foreground color changed to {AdvancedConsole.ForegroundColor}");
            }
            catch
            {
                AdvancedConsole.Warn("Wrong color name!");
            }
        }

        [Command(Prefix, "change-background-color", "Sets the background color of the console.")]
        public static void ChangeBackgroundColorCommand(string colorName)
        {
            try
            {
                AdvancedConsole.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorName, true);
                AdvancedConsole.WriteLine($"Background color changed to {AdvancedConsole.BackgroundColor}");
            }
            catch
            {
                AdvancedConsole.Warn("Wrong color name!"); 
            }
        }

        [Command(Prefix, "reset-color", "Sets the foreground and background console colors to their defaults.")]
        public static void ResetColorCommand()
        {
            Console.ResetColor();
            AdvancedConsole.WriteLine("Foreground and background console colors are reset to their default values"); 
        }

        [Command(Prefix, "clear", "Clears the console.")]
        public static void ClearCommand()
        {
            Console.Clear();
            AdvancedConsole.WriteLine("The console cleared");
        }

        [Command(Prefix, "steins-gate", "???")]
        public static void SteinsGateCommand()
        {
            string musicPath, artPath;

            #if DEBUG
                artPath = @"..\..\..\AsciiArts\SteinsGate.txt";
                musicPath = @"..\..\..\ConsoleMusic\HackingToTheGate.xml";
            #else
                artPath = @"AsciiArts\SteinsGate.txt";
                musicPath = @"ConsoleMusic\HackingToTheGate.xml";
            #endif

            try
            {
                if (_hackingToTheGateTune == null)
                {
                    using FileStream stream = new FileStream(musicPath, FileMode.Open);
                    _hackingToTheGateTune = (Tune)TuneSerializer.Deserialize(stream);
                }
                Tune tune = _hackingToTheGateTune;

                using (StreamReader reader = new StreamReader(artPath))
                {
                    ShowWindow(ThisConsole, (int)ShowWindowType.Maximize);

                    tune.Play(() =>
                    {
                        if (reader.Peek() > -1)
                        {
                            Console.WriteLine(reader.ReadLine());
                        }
                    });

                    ShowWindow(ThisConsole, (int)ShowWindowType.Restore);
                }
            }
            catch
            {
                AdvancedConsole.Warn("Command can't be executed!");
            }
        }
    }
}
