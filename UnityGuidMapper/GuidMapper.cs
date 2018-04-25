using System;
using System.Collections.Generic;
using System.IO;

namespace UnityGuidMapper
{
    public class GuidMapper
    {
        public string AssetsDirectory { get; private set; }
        public string TargetGuid { get; private set; }
        public string NewGuid { get; private set; }

        private List<string> m_Files;
        private List<string> m_FilteredFiles = new List<string>();

        public GuidMapper(string assetsDirectory)
        {
            AssetsDirectory = assetsDirectory;
            m_Files = FindYamlFiles(AssetsDirectory);
        }

        public void FilterBy(string guid)
        {
            // Find files that contains given GUID.
            TargetGuid = "";
            m_FilteredFiles.Clear();

            // Just clear filter.
            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            // Filter by guid.
            TargetGuid = guid;
            foreach (var file in m_Files)
            {
                var fileText = File.ReadAllText(file);
                if (fileText.Contains(GetGuidExpression(TargetGuid)))
                {
                    m_FilteredFiles.Add(file);
                }
            }
        }

        public void ReplaceWith(string guid)
        {
            // Replace old GUID with the new GUID given.
            foreach (var file in m_FilteredFiles)
            {
                var fileText = File.ReadAllText(file);
                fileText = fileText.Replace(GetGuidExpression(TargetGuid), GetGuidExpression(guid));
                File.WriteAllText(file, fileText);
            }
        }

        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine($"Total {m_FilteredFiles.Count} assets found:");
            foreach (var file in m_FilteredFiles)
            {
                Console.WriteLine("\t" + Path.GetFileName(file));
            }
        }

        private static List<string> FindYamlFiles(string path)
        {
            return FileSearch.FindFiles(path, (file) =>
            {
                using (var readingFile = new StreamReader(file))
                {
                    var firstLine = readingFile.ReadLine();
                    return firstLine.Contains("%YAML");
                }
            });
        }

        private static string GetGuidExpression(string guid)
        {
            return $"guid: {guid}";
        }
    }
}
