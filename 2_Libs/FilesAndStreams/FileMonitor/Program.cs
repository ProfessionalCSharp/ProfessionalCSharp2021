FileSystemWatcher? _watcher;

if (args == null || args.Length != 1)
{
    Console.WriteLine("Enter the directory to watch markdown files: FileMonitor [directory]");
    return;
}

WatchFiles(args[0], "*.md");
Console.WriteLine("Press enter to stop watching");
Console.ReadLine();
UnWatchFiles();

void WatchFiles(string path, string filter)
{
    _watcher = new(path, filter)
    {
        IncludeSubdirectories = true,
    };
    _watcher.Created += OnFileChanged;
    _watcher.Changed += OnFileChanged;
    _watcher.Deleted += OnFileChanged;
    _watcher.Renamed += OnFileRenamed;

    _watcher.EnableRaisingEvents = true;
    Console.WriteLine("watching file changes...");
}

void UnWatchFiles()
{
    if (_watcher == null) throw new InvalidOperationException();

    _watcher.Created -= OnFileChanged;
    _watcher.Changed -= OnFileChanged;
    _watcher.Deleted -= OnFileChanged;
    _watcher.Renamed -= OnFileRenamed;
    _watcher.Dispose();
    _watcher = null;
}

void OnFileRenamed(object sender, RenamedEventArgs e) =>
   Console.WriteLine($"file {e.OldName} {e.ChangeType} to {e.Name}");

void OnFileChanged(object sender, FileSystemEventArgs e) =>
    Console.WriteLine($"file {e.Name} {e.ChangeType}");
