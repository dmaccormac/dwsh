using Microsoft.Win32;
namespace dwsh
{
    internal static class RegistryHelper
    {
        public static void CreateRegistryKey(string keyPath, string valueData)
        {

            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(keyPath))
                {
                    if (registryKey != null)
                    {
                        registryKey.SetValue(null, valueData);
                        Console.WriteLine($"Created registry key '{keyPath}'");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to create the Registry key '{keyPath}'");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating registry key '{keyPath}': {ex.Message}");
            }
        }
        public static void DeleteRegistryKey(string keyName)
        {
            try
            {
                RegistryKey baseRegistryKey = Registry.CurrentUser;
                RegistryKey? keyToDelete = baseRegistryKey.OpenSubKey(keyName, true);

                if (keyToDelete != null)
                {
                    baseRegistryKey.DeleteSubKeyTree(keyName);
                    Console.WriteLine($"Deleted registry key '{keyName}'");
                }
                else
                {
                    Console.WriteLine($"Registry key '{keyName}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting registry key '{keyName}': {ex.Message}");
            }
        }
    }
}
