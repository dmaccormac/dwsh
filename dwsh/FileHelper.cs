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
                Console.WriteLine($"Error searching for file: {ex.Message}");
            }

            return results;
        }

        public static string ReadTextFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using StreamWriter writer = File.CreateText(filePath);
                }

                string fileContents;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    fileContents = reader.ReadToEnd();
                }

                return fileContents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                throw;

            }
        }


        public static void AppendTextToFile(string filePath, string lineToAdd)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine(lineToAdd);
                    }
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(lineToAdd);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
    }
}
