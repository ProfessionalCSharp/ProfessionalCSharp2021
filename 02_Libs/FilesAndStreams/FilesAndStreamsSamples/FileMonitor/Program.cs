using System;
using System.IO;

namespace FileMonitor
{
    public class Program
    {
        private static FileSystemWatcher? s_watcher;

        public static void Main()
        {
            WatchFiles(".", "*.txt");
            Console.ReadLine();
            UnWatchFiles();
        }

        public static void WatchFiles(string path, string filter)
        {
            s_watcher = new FileSystemWatcher(path, filter)
            {
                IncludeSubdirectories = true
            };
            s_watcher.Created += OnFileChanged;
            s_watcher.Changed += OnFileChanged;
            s_watcher.Deleted += OnFileChanged;
            s_watcher.Renamed += OnFileRenamed;

            s_watcher.EnableRaisingEvents = true;
            Console.WriteLine("watching file changes...");
        }

        public static void UnWatchFiles()
        {
            if (s_watcher == null) throw new InvalidOperationException();

            s_watcher.Created -= OnFileChanged;
            s_watcher.Changed -= OnFileChanged;
            s_watcher.Deleted -= OnFileChanged;
            s_watcher.Renamed -= OnFileRenamed;
            s_watcher.Dispose();
            s_watcher = null;
        }

        private static void OnFileRenamed(object sender, RenamedEventArgs e) =>
            Console.WriteLine($"file {e.OldName} {e.ChangeType} to {e.Name}");

        private static void OnFileChanged(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"file {e.Name} {e.ChangeType}");
    }
}
