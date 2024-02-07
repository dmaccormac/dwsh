using System.Collections;

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
        public static List<string?> SearchForFile(string filename, string[] searchDirectories)
        {
            var results = new List<string?>();

            try
            {
                foreach (string directory in searchDirectories)
                {
                    String[] files = Directory.GetFiles(directory, filename, SearchOption.AllDirectories);
                    foreach (string file in files)
                        results.Add(Path.GetDirectoryName(file));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            return results;
        }
    }
}
