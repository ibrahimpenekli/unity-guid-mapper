using System;
using System.IO;

namespace UnityGuidMapper
{
    public class ShowCommand : ProgramCommand
    {
        private const string GuidKey = "guid:";

        public string AssetFile => Args[0];
        public override string Usage => $"{Name} <{nameof(AssetFile)}>";
        public override string Description => "Show the GUID for the asset file.";

        public ShowCommand() : base("-show", 1)
        {
        }

        public override void Run()
        {
            Console.WriteLine();
            Console.WriteLine($"Assets file : {AssetFile}");

            var metaFilePath = AssetFile + ".meta";
            if (File.Exists(metaFilePath))
            {
                try
                {
                    var metaText = File.ReadAllText(metaFilePath);
                    var startIdx = metaText.IndexOf(GuidKey) + GuidKey.Length;
                    var endIdx = metaText.IndexOf("\r\n", startIdx);
                    var guid = metaText.Substring(startIdx, endIdx - startIdx).Trim();
                    Console.WriteLine($"Asset GUID: {guid}");
                }
                catch (Exception)
                {
                    Console.WriteLine($"error: Meta file is not valid.");
                }
            }
            else
            {
                Console.WriteLine($"error: Meta file not exist for the file.");
            }
        }
    }
}
