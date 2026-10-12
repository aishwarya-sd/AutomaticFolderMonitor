
using System;
using System.IO;

namespace AutomaticFolderMonitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\FolderMonitor\Watch";

            try
            {
                // Create the folder if it does not exist.
                Directory.CreateDirectory(folderPath);

                // Create the folder watcher.
                using FileSystemWatcher watcher =
                    new FileSystemWatcher(folderPath);

                // Monitor file names, file size and modifications.
                watcher.NotifyFilter =
                    NotifyFilters.FileName |
                    NotifyFilters.LastWrite |
                    NotifyFilters.Size;

                // Monitor files in this folder only.
                watcher.IncludeSubdirectories = false;

                // Listen for file events.
                watcher.Created += OnCreated;
                watcher.Changed += OnChanged;
                watcher.Deleted += OnDeleted;
                watcher.Renamed += OnRenamed;
                watcher.Error += OnError;

                // Start monitoring.
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("=================================");
                Console.WriteLine("    AUTOMATIC FOLDER MONITOR");
                Console.WriteLine("=================================");
                Console.WriteLine($"Monitoring: {folderPath}");
                Console.WriteLine("Create, edit, rename or delete files.");
                Console.WriteLine("Press Q to stop monitoring.");
                Console.WriteLine();

                // Keep the application running.
                while (Console.ReadKey(true).Key != ConsoleKey.Q)
                {
                    // Wait for the Q key.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Folder monitoring stopped.");
        }

        static void OnCreated(object sender, FileSystemEventArgs e)
        {
            PrintEvent("FILE CREATED", e.Name);
        }

        static void OnChanged(object sender, FileSystemEventArgs e)
        {
            PrintEvent("FILE MODIFIED", e.Name);
        }

        static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            PrintEvent("FILE DELETED", e.Name);
        }

        static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine(
                $"[{DateTime.Now:HH:mm:ss}] FILE RENAMED: " +
                $"{e.OldName} -> {e.Name}");
        }

        static void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(
                $"Monitoring error: {e.GetException().Message}");
        }

        static void PrintEvent(string action, string? fileName)
        {
            Console.WriteLine(
                $"[{DateTime.Now:HH:mm:ss}] {action}: {fileName}");
        }
    }
}
