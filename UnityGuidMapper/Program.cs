using System;
using System.Linq;

namespace UnityGuidMapper
{
    class Program
    {
        private static readonly ProgramCommand[] Commands = new ProgramCommand[]
        {
            new ShowCommand(),
            new ListCommand(),
            new ReplaceCommand()
        };

        static void Main(string[] args)
        {
            var command = Commands.FirstOrDefault(c => c.TryParse(args));
            if (command == null)
            {
                PrintUsage();
                return;
            }

            command.Run();
#if DEBUG
            Console.WriteLine("Done! Press enter to exit...");
            Console.ReadLine();
#endif
        }

        private static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            foreach (var command in Commands)
            {
                Console.WriteLine($"dotnet uguid.dll {command.Usage}");
                Console.WriteLine($"\t{command.Description}");
                Console.WriteLine();
            }
        }
    }
}
