using System;
using System.Collections.Generic;
using System.IO;

namespace UnityGuidMapper
{
    public class FileSearch
    {
        public static List<string> FindFiles(string path, Predicate<string> predicate)
        {
            var files = new List<string>();
            FindFilesRecursively(path, files, predicate);
            return files;
        }

        private static void FindFilesRecursively(string path, List<string> files, Predicate<string> predicate)
        {
            foreach (var d in Directory.GetDirectories(path))
            {
                foreach (var f in Directory.GetFiles(d))
                {
                    if (predicate.Invoke(f))
                    {
                        files.Add(f);
                    }
                }
                FindFilesRecursively(d, files, predicate);
            }
        }
    }
}
