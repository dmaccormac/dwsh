using System.Diagnostics;
using Microsoft.Win32;
using static System.Environment;

namespace dwsh
{
    internal static class Installer
    {

        public static void Run(string damewareDirectory) {

            Console.WriteLine("Install dwsh...");
            Console.WriteLine("Directory: " + damewareDirectory);

            
            try
            {
                // add .dwc file association to registry

                Console.Write("Adding registry entries...");
                string keyName = ".dwc";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                key.CreateSubKey(keyName);
                key = key.OpenSubKey(keyName, true);
                key.SetValue("", "DamewareConnection");

                string exeName = Process.GetCurrentProcess().ProcessName + ".exe";
                string ShellCmd = damewareDirectory + @"\" + exeName + " connect -file %1";
                keyName = @"DamewareConnection\Shell\open\command";
                key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                key.CreateSubKey(keyName);
                key = key.OpenSubKey(keyName, true);
                key.SetValue("", ShellCmd);

                keyName = @"DamewareConnection\DefaultIcon";
                key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                key.CreateSubKey(keyName);
                key = key.OpenSubKey(keyName, true);
                key.SetValue("", Path.Combine(damewareDirectory, Config.DamewareExecutable));
                Console.WriteLine("OK");


                // add DamewareDirectory to user PATH variable
                Console.Write("Setting environment variables...");
                string path = GetEnvironmentVariable("Path", EnvironmentVariableTarget.User) + ";" + damewareDirectory;
                SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.User);
                Console.WriteLine("OK");


                // copy dwsh.exe to DameWareDirectory
                Console.Write("Copying files...");
                string fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
                string filePath = Path.Combine(CurrentDirectory, fileName);
                File.Copy(filePath, Path.Combine(damewareDirectory, fileName), true); //overwrite true
                Console.WriteLine("OK");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(e.Message);
            }


        }
    }
}
