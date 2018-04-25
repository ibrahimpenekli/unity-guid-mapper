using System;

namespace UnityGuidMapper
{
    public class ReplaceCommand : ProgramCommand
    {
        public string AssetsDirectory => Args[0];
        public string OldGuid => Args[1];
        public string NewGuid => Args[2];
        public override string Usage => $"{Name} <{nameof(AssetsDirectory)}> <{nameof(OldGuid)}> <{nameof(NewGuid)}> ";
        public override string Description => "Replaces assets that uses given GUID with the new one.";

        public ReplaceCommand() : base("-replace", 3)
        {
        }

        public override void Run()
        {
            Console.WriteLine();
            Console.WriteLine($"Assets Directory : {AssetsDirectory}");
            Console.WriteLine($"GUID (old)       : {OldGuid}");
            Console.WriteLine($"GUID (new)       : {NewGuid}");

            var guidMapper = new GuidMapper(AssetsDirectory);
            guidMapper.FilterBy(OldGuid);
            guidMapper.Print();

            if (ConfirmByUser())
            {
                guidMapper.ReplaceWith(NewGuid);
            }
        }

        private static bool ConfirmByUser()
        {
            Console.Write($"Continue to replace? (Y)   : ");
            return Console.ReadLine().ToUpper() == "Y";
        }
    }
}
