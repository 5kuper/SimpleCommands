## About branch
**This branch contains code that edited for build .NET Standard 2.0 libraries.**

## Navigation
- **SCS.System** - Core library
- **SampleCommands** - Libraries with sample commands
- **SampleConsoleApp** - Sample preparing commands for use
  - **AsciiArts** - Ascii art for a command from ConsoleCommands
  - **ConsoleMusic** - XML with music for a command from ConsoleCommands
- **ConsoleMusic** - Library for play a music using Console.Beep()


## Create commands
```CSharp
using System;
using SCS.System;

namespace SCS.Commands
{
	public class SampleCommands
	{
		public const string Prefix = "s!";

		private SampleCommands() { }

		// Don't use spaces for the command name.
		// Use null in the prefix/description to set the standard value. 
		[Command(Prefix, "sample-command-name", "Sample command description.")]
		public static void SampleCommand(string text) // The method must be static!
		{
			AdvancedConsole.WriteLine(text);
		}
	}
}
```

Command methods can have overloaded versions and parameters with default values.

You can add multiple attributes to a method so that the command can be invoked with multiple names.


## Prepare commands for use 
[Program.cs](SampleConsoleApp/Program.cs)


## Use commands
![LOOKATME.gif](LOOKATME.gif)
