namespace dwsh
{
    internal static class PathHelper
    {

        public static void AddEntryToUserPath(string entryToAdd)
        {
            try
            {
                string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);

                if (!currentPath.Split(';').Contains(entryToAdd, StringComparer.OrdinalIgnoreCase))
                {
                    string newPath = currentPath + ";" + entryToAdd;
                    Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
                    Console.WriteLine($"Entry '{entryToAdd}' added to the PATH variable.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry to the PATH variable: {ex.Message}");
            }
        }
        public static void RemoveEntryFromUserPath(string entryToRemove)
        {
            try
            {

                string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
                string[] pathEntries = currentPath.Split(';');
                string updatedPath = string.Join(";", pathEntries.Where(entry => entry != entryToRemove));
                Environment.SetEnvironmentVariable("PATH", updatedPath, EnvironmentVariableTarget.User);
                Console.WriteLine($"Entry '{entryToRemove}' removed from the PATH variable." );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing entry from the PATH variable: {ex.Message}") ;
            }
        }
    }
}
