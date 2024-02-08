using System.Diagnostics;
using static dwsh.RegistryHelper;
using static dwsh.PathHelper;
using static dwsh.FileHelper;

namespace dwsh
{

    internal static class Installer
    {
        private static string _installTarget = "";
        private static string _currentProcess = "";

        public static void Run(string damewareDirectory)
        {
            _installTarget = damewareDirectory;
            _currentProcess = Process.GetCurrentProcess().ProcessName + ".exe";

            Console.WriteLine("Installing dwsh...");
            Console.WriteLine("Target directory: " + _installTarget);

            CreateFileAssociation();
            CopyIntallationFiles();
            AddEntriesToUserPath();

        }

        private static void CreateFileAssociation()
        {
            var registryEntries = new Dictionary<string, string>
            {
                {
                    @"Software\Classes\.dwc",
                    "DamewareConnection"
                },
                {
                    @"Software\Classes\DamewareConnection\Shell\open\command",
                    $"{Path.Combine(_installTarget, _currentProcess)} connect -file %1"
                },
                {
                    @"Software\Classes\DamewareConnection\DefaultIcon",
                    Path.Combine(_installTarget, Config.DamewareExecutable)
                }
            };


            foreach (var entry in registryEntries)
                CreateRegistryKey(entry.Key, entry.Value);

        }

        private static void CopyIntallationFiles()
        {
            var fileMappings = new Dictionary<string, string>
            {
                { _currentProcess, Path.Combine(_installTarget, _currentProcess) }
                
            };


            foreach (var mapping in fileMappings)
            {
                var sourceFilePath = mapping.Key;
                var destinationFilePath = mapping.Value;
                CopyFile(sourceFilePath, destinationFilePath);
            }

        }

        private static void AddEntriesToUserPath()
        {
            string[] userPathEntries = { _installTarget };

            foreach (var entry in userPathEntries)
                AddEntryToUserPath(entry);

        }

    }
}
