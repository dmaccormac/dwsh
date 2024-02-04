using Microsoft.Win32;
using static System.Environment;

namespace dwsh
{
    internal class Uninstaller
    {
        public Uninstaller(string damewareDirectory) {

            Console.WriteLine("Uninstall dwsh...");
            Console.WriteLine("Directory: " + damewareDirectory);

            //remove registry writes
            try
            {
                Console.Write("Removing registry entries...");
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
                key.DeleteSubKey(".dwc", false);
                key.DeleteSubKeyTree("DamewareConnection", false);
                Console.WriteLine("OK");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(e.Message);
            }

            //remove DamewareDirectory from user PATH variable
            try
            {

                Console.Write("Removing environment variable entries...");

                string path = GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
                while (path.Contains(damewareDirectory))
                    path = path.Replace(";" + damewareDirectory, "");

                Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.User);
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
