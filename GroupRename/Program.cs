using System;
using System.IO;
using System.Linq;

Console.WriteLine("This script helps to rename files in group");

string folderPath = Path.Join(@"D:\photo\2019");

// string suffix = "compressed";

// try
// {
//     foreach (string file in Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories))
//     {
//         Console.WriteLine(file); 

//         string? directory = Path.GetDirectoryName(file);
//         string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);
//         string extension = Path.GetExtension(file);

//         string newFileName = $"{fileNameWithoutExt}_{suffix}{extension}";
//         string newPath = Path.Combine(directory ?? folderPath, newFileName);

//         File.Move(file, newPath);
//         Console.WriteLine($"Renamed File: {file} -> {newPath}");
//     }
// }
// catch (UnauthorizedAccessException ex)
// {
//     Console.WriteLine($"Access denied: {ex.Message}");
// }
// catch (DirectoryNotFoundException ex)
// {
//     Console.WriteLine($"Directory not found: {ex.Message}");
// }




try
{
    // Get immediate subdirectories, ordered by length descending to rename deepest folders first if nested
    foreach (string subDir in Directory.EnumerateDirectories(folderPath))
    {
        string? parentDir = Path.GetDirectoryName(subDir);
        string folderName = Path.GetFileName(subDir);

        // Replace underscores with hyphens in the folder name
        string newFolderName = folderName.Replace('_', '-');
        string newPath = Path.Combine(parentDir ?? folderPath, newFolderName);

        // Rename only if the name actually changes
        if (!subDir.Equals(newPath, StringComparison.OrdinalIgnoreCase))
        {
            Directory.Move(subDir, newPath);
            Console.WriteLine($"Renamed Folder: {folderName} -> {newFolderName}");
        }
    }
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"Access denied: {ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"IO error (folder might be in use): {ex.Message}");
}
