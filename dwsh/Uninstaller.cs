using static dwsh.RegistryHelper;
using static dwsh.PathHelper;

namespace dwsh
{
    internal static class Uninstaller
    {
        public static void Run(string damewareDirectory) {

            Console.WriteLine("Uninstall dwsh...");
            Console.WriteLine("Directory: " + damewareDirectory);

            DeleteRegistryKey(@"Software\Classes\.dwc");
            DeleteRegistryKey(@"Software\Classes\DamewareConnection");
            RemoveEntryFromUserPath(damewareDirectory);

        }
    }
}
