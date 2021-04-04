using System;
using System.Collections.Generic;

namespace SCS.System
{
    public abstract class CommandScanner // Used as filter for the Command.Find method
    {
        /// <summary>The enum of all command elements that can be scanned.</summary>
        public enum TargetOfScanner { Prefix, Name, Description, Tags, Method, Class, MethodName, ClassName, ContainsParameters }

        /// <summary>The element of a command that will be scanned.</summary>
        public TargetOfScanner TargetOfScan { get; }

        protected CommandScanner(TargetOfScanner targetOfScan)
        {
            TargetOfScan = targetOfScan;
        }

        /// <summary>Scans the command using the TargetOfScan, ScanValue and ScanCondition specified in the constructor and returns result.</summary>
        public bool Scan(Command command)
        {
            bool result = false;

            switch (TargetOfScan)
            {
                case TargetOfScanner.Prefix:
                    result = Scan(command.Prefix);
                    break;
                case TargetOfScanner.Name:
                    result = Scan(command.Name);
                    break;
                case TargetOfScanner.Description:
                    result = Scan(command.Description);
                    break;
                case TargetOfScanner.Tags:
                    result = Scan(command.Tags);
                    break;
                case TargetOfScanner.Method:
                    result = Scan(command.Method);
                    break;
                case TargetOfScanner.Class:
                    result = Scan(command.Class);
                    break;
                case TargetOfScanner.MethodName:
                    result = Scan(command.Method.Name);
                    break;
                case TargetOfScanner.ClassName:
                    result = Scan(command.Class.Name);
                    break;
                case TargetOfScanner.ContainsParameters:
                    result = Scan(command.ContainsParameters);
                    break;
            }

            return result;
        }

        protected abstract bool Scan(object valueOfTarget);
    }

    public class SimpleCommandScanner : CommandScanner // or ObjectCommandScanner
    {
        /// <summary>The enum of command elements that can be scanned by this scanner.</summary>
        public new enum TargetOfScanner { Prefix, Name, Description, Tags, Method, Class, MethodName, ClassName, ContainsParameters }

        /// <summary>The enum of scanning conditions supported by this scanner.</summary>
        public enum ScannerCondition { Equals, NotEquals }

        /// <summary>The value that will be used for scan the TargetOfScan.</summary>
        public object ScanValue { get; }

        /// <summary>The condition that will be used for scan the TargetOfScan.</summary>
        public ScannerCondition ScanCondition { get; }

        public SimpleCommandScanner(object scanValue, TargetOfScanner scanTarget, ScannerCondition scanCondition = ScannerCondition.Equals)
            : base((CommandScanner.TargetOfScanner)Enum.Parse(typeof(CommandScanner.TargetOfScanner), scanTarget.ToString()))
            // Converting value of this TargetOfScanner to value of the base TargetOfScanner 
        {
            ScanValue = scanValue;
            ScanCondition = scanCondition;
        }

        protected override bool Scan(object valueOfTarget)
        {
            bool result = Object.Equals(valueOfTarget, ScanValue);
            return ScanCondition != ScannerCondition.NotEquals ? result : !result;
        }
    }

    public class StringCommandScanner : CommandScanner
    {
        /// <summary>The enum of command elements that can be scanned by this scanner.</summary>
        public new enum TargetOfScanner { Prefix, Name, Description, MethodName, ClassName }

        /// <summary>The enum of scanning conditions supported by this scanner.</summary>
        public enum ScannerCondition { Equals, NotEquals, Contains, NotContains }

        /// <summary>The value that will be used for scan the TargetOfScan.</summary>
        public string ScanValue { get; }

        /// <summary>The condition that will be used for scan the TargetOfScan.</summary>
        public ScannerCondition ScanCondition { get; }

        public StringCommandScanner(string scanValue, TargetOfScanner scanTarget, ScannerCondition scanCondition = ScannerCondition.Equals)
            : base((CommandScanner.TargetOfScanner)Enum.Parse(typeof(CommandScanner.TargetOfScanner), scanTarget.ToString()))
            // Converting value of this TargetOfScanner to value of the base TargetOfScanner 
        {
            ScanValue = scanValue;
            ScanCondition = scanCondition;
        }

        protected override bool Scan(object valueOfTarget)
        {
            bool result;

            if (ScanCondition == ScannerCondition.Equals || ScanCondition == ScannerCondition.NotEquals)
            {
                result = (string)valueOfTarget == ScanValue;
            }
            else
            {
                result = Convert.ToString(valueOfTarget).Contains(ScanValue);
            }

            if (ScanCondition == ScannerCondition.NotEquals || ScanCondition == ScannerCondition.NotContains)
            {
                result = !result;
            }

            return result;
        }
    }

    public class ListCommandScanner : CommandScanner
    {
        /// <summary>The enum of command elements that can be scanned by this scanner.</summary>
        public new enum TargetOfScanner { Tags }

        /// <summary>The enum of scanning conditions supported by this scanner.</summary>
        public enum ScannerCondition { Contains, NotContains }

        /// <summary>The value that will be used for scan the TargetOfScan.</summary>
        public object ScanValue { get; }

        /// <summary>The condition that will be used for scan the TargetOfScan.</summary>
        public ScannerCondition ScanCondition { get; }

        public ListCommandScanner(object scanValue, TargetOfScanner scanTarget = TargetOfScanner.Tags, ScannerCondition scanCondition = ScannerCondition.Contains)
            : base((CommandScanner.TargetOfScanner)Enum.Parse(typeof(CommandScanner.TargetOfScanner), scanTarget.ToString()))
            // Converting value of this TargetOfScanner to value of the base TargetOfScanner 
        {
            ScanValue = scanValue;
            ScanCondition = scanCondition;
        }

        protected override bool Scan(object valueOfTarget)
        {
            List<object> objectList = new List<object>();

            if (TargetOfScan == CommandScanner.TargetOfScanner.Tags)
            {
                objectList.AddRange((List<string>)valueOfTarget);
            }

            bool result = objectList.Contains(ScanValue);

            return ScanCondition != ScannerCondition.NotContains ? result : !result;
        }
    }
}
