using System;

namespace SCS.System
{
    public abstract class CommandScanner
    {
        public enum TargetOfScanner { Prefix, Name, Description, Tags, Method, Class, MethodName, ClassName, Parameters, ContainsParameters }

        public TargetOfScanner TargetOfScan { get; private set; }

        protected CommandScanner(TargetOfScanner targetOfScan)
        {
            TargetOfScan = targetOfScan;
        }

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
                case TargetOfScanner.Parameters:
                    result = Scan(command.Parameters);
                    break;
                case TargetOfScanner.ContainsParameters:
                    result = Scan(command.ContainsParameters);
                    break;
            }

            return result;
        }

        public abstract bool Scan(object valueOfTarget);
    }

    public class SimpleCommandScanner : CommandScanner
    {
        new public enum TargetOfScanner { Prefix, Name, Description, Tags, Method, Class, MethodName, ClassName, Parameters, ContainsParameters }
        public enum ScannerCondition { Equals, NotEquals }

        public object ScanValue { get; private set; }
        public ScannerCondition ScanCondition { get; private set; }

        public SimpleCommandScanner(object scanValue, TargetOfScanner scanTarget = TargetOfScanner.Name, ScannerCondition scanCondition = ScannerCondition.Equals) 
            : base((CommandScanner.TargetOfScanner)scanTarget) 
        {
            ScanValue = scanValue;
            ScanCondition = scanCondition;
        }

        public override bool Scan(object valueOfTarget)
        {
            bool result = valueOfTarget == ScanValue;
            if (ScanCondition == ScannerCondition.NotEquals)
            {
                result = !result;
            }
            return result;
        }
    }

    public class StringCommandScanner : CommandScanner
    {
        new public enum TargetOfScanner { Prefix, Name, Description, MethodName, ClassName }
        public enum ScannerCondition { Equals, NotEquals, Contains, NotContains }

        public string ScanValue { get; private set; }
        public ScannerCondition ScanCondition { get; private set; }

        public StringCommandScanner(string scanValue, TargetOfScanner scanTarget = TargetOfScanner.Name, ScannerCondition scanCondition = ScannerCondition.Equals) 
            : base((CommandScanner.TargetOfScanner)scanTarget)
        {
            ScanValue = scanValue;
            ScanCondition = scanCondition;
        }

        public override bool Scan(object valueOfTarget)
        {
            bool result = false;

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

        // TODO: ArrayCommandScanner
    }
}
