using static dwsh.RegistryHelper;
using static dwsh.PathHelper;

namespace dwsh
{
    internal static class Uninstaller
    {
        public static void Run(string damewareDirectory) {

            Console.WriteLine("Uninstalling dwsh...");
            Console.WriteLine("Target directory: " + damewareDirectory);

            DeleteRegistryKey(@"Software\Classes\.dwc");
            DeleteRegistryKey(@"Software\Classes\DamewareConnection");
            RemoveEntryFromUserPath(damewareDirectory);

        }
    }
}
