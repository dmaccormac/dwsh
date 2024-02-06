namespace dwsh
{
    internal static class FileHelper
    {

        public static void CopyFile(string sourceFilePath, string destinationFilePath)
        {

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
        public static string? TryGetDamewareDirectory()
        {
            try
            {
                string[] directories = [Environment.ExpandEnvironmentVariables("%ProgramW6432%"),
                    Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%")];

                foreach (string directory in directories)
                {
                    String[] files = Directory.GetFiles(directory, Config.DamewareExecutable, SearchOption.AllDirectories);
                    foreach (string file in files)
                        return Path.GetDirectoryName(file);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Dameware directory: {ex.Message}");
            }

            return null;
        }
    }
}
