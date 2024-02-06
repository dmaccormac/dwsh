using System.Diagnostics;
using Microsoft.Win32;

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
            setEnvironmentVariables();
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
            {
                try
                {
                    CreateRegistryKey(entry.Key, entry.Value);
                    Console.WriteLine($"Registry key created successfully: {entry.Key}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            

        }

        private static void CopyIntallationFiles()
        {
            
            string sourceFilePath = _currentProcess;
            string destinationFilePath = Path.Combine(_installTarget, _currentProcess);
            try
            {
                CopyFile(sourceFilePath, destinationFilePath);
                Console.WriteLine("File(s) copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void setEnvironmentVariables()
        {
            try
            {
                AddEntryToUserPath(_installTarget);
                Console.WriteLine("Environment variables updated successfully.");
            }
   
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    
        private static void CreateRegistryKey(string keyPath, string valueData)
        {

            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(keyPath))
                {
                    if (registryKey != null)
                    {
                        registryKey.SetValue(null, valueData);
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to create the registry key.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating registry key and setting value: {ex.Message}");
            }
        }



        private static void CopyFile(string sourcePath, string destinationPath)
        {
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException("Source file not found.", sourcePath);
            }

            try
            {
                File.Copy(sourcePath, destinationPath, true);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error copying file: {ex.Message}");
            }
        }

        static void AddEntryToUserPath(string newPathEntry)
        {
            try
            {
                string? currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

                if (currentPath == null)
                    throw new InvalidOperationException("Error adding entry to user PATH");

                if (!currentPath.Split(';').Contains(newPathEntry, StringComparer.OrdinalIgnoreCase))
                {
                    string newPath = currentPath + ";" + newPathEntry;
                    Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding entry to user PATH: {ex.Message}");
            }
        }
    }
}
