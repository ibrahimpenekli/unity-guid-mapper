using System;

namespace UnityGuidMapper
{
    public class ListCommand : ProgramCommand
    {
        public string AssetsDirectory => Args[0];
        public string Guid => Args[1];
        public override string Usage => $"{Name} <{nameof(AssetsDirectory)}> <{nameof(Guid)}>";
        public override string Description => "Lists assets that uses given GUID asset.";

        public ListCommand() : base("-list", 2)
        {
        }

        public override void Run()
        {
            Console.WriteLine();
            Console.WriteLine($"Assets Directory : {AssetsDirectory}");
            Console.WriteLine($"GUID             : {Guid}");

            var guidMapper = new GuidMapper(AssetsDirectory);
            guidMapper.FilterBy(Guid);
            guidMapper.Print();
        }
    }
}
