using System;
using System.IO;

namespace BankingFileGenerator.Lib.Utils
{
    public static class FileWriter
    {
        /// <summary>
        /// Saves the provided content to a uniquely named, timestamped text file in the specified directory.
        /// </summary>
        /// <param name="content">The file content to write.</param>
        /// <param name="prefix">The filename prefix. Default is "BAI2".</param>
        /// <param name="path">The directory to save the file in. Default is "output".</param>
        /// <returns>The full path of the saved file.</returns>
        public static string SaveFile(string content, string prefix = "BAI2", string path = "output")
        {
            // Validate input arguments
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be null or whitespace.", nameof(content));
            if (string.IsNullOrWhiteSpace(prefix))
                prefix = "BAI2";
            if (string.IsNullOrWhiteSpace(path))
                path = "output";

            // Ensure the output directory exists
            Directory.CreateDirectory(path);

            // Generate a timestamp with milliseconds for uniqueness
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");

            // Compose the filename and full path
            string filename = $"{prefix}_{timestamp}.txt";
            string fullPath = Path.Combine(path, filename);

            // Write the content to the file using UTF-8 encoding (default)
            File.WriteAllText(fullPath, content);

            // Return the full file path
            return fullPath;
        }
    }
}
