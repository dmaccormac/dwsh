using System.Diagnostics;
using static dwsh.RegistryHelper;
using static dwsh.PathHelper;

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

            Console.WriteLine("Install dwsh...");
            Console.WriteLine("Directory: " + _installTarget);

            CreateFileAssociation();
            CopyIntallationFiles();
            AddEntryToUserPath(_installTarget);
        }

        private static void CreateFileAssociation()
        {
            var entries = new Dictionary<string, string>
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


            foreach (var entry in entries)
                CreateRegistryKey(entry.Key, entry.Value);

        }

        private static void CopyIntallationFiles()
        {
            
            string sourceFilePath = _currentProcess;
            string destinationFilePath = Path.Combine(_installTarget, _currentProcess);
         
            try
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                Console.WriteLine($"Copied file {sourceFilePath} to {destinationFilePath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying file: {ex.Message}");
            }
        }

    }
}
