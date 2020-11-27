using System;

namespace SCS.System
{
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
