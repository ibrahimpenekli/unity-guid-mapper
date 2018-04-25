using System;

namespace UnityGuidMapper
{
    public abstract class ProgramCommand
    {
        public string Name { get; set; }
        public int ArgsCount { get; set; }
        public string[] Args { get; private set; }

        public ProgramCommand(string name, int argsCount)
        {
            Name = name;
            ArgsCount = argsCount;
            Args = new string[argsCount];
        }

        public virtual bool TryParse(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }

            if (Name != args[0])
            {
                return false;
            }

            if (args.Length != ArgsCount + 1)
            {
                return false;
            }

            Array.Copy(args, 1, Args, 0, Args.Length);
            return true;
        }

        public abstract void Run();
        public abstract string Usage { get; }
        public abstract string Description { get; }
    }
}
