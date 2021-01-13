using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;

await BuildCommandLine()
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

CommandLineBuilder BuildCommandLine()
{
    Option<string> fileOption = new("--file")
    {
        IsRequired = true
    };
    RootCommand rootCommand = new("FilesAndFolders");
    Command showDrivesCommand = new("showdrives") { Handler = CommandHandler.Create(ShowDrivesInformation) };
    Command specialFoldersCommand = new("specialfolders") { Handler = CommandHandler.Create(ShowSpecialFolders) };
    Command createFileCommand = new("createfile") { fileOption };
    createFileCommand.Handler = CommandHandler.Create<string>(CreateFile);
    Command fileInfoCommand = new("fileinfo") { fileOption };
    fileInfoCommand.Handler = CommandHandler.Create<string>(FileInformation);
    Command changePropertiesCommand = new("changeprops") { fileOption };
    changePropertiesCommand.Handler = CommandHandler.Create<string>(ChangeFileProperties);
    Command readLinesCommand = new("readlines") { fileOption };
    readLinesCommand.Handler = CommandHandler.Create<string>(ReadLineByLine);
    Command writeFileCommand = new("writefile") { Handler = CommandHandler.Create(WriteAFile) };
    Command deleteDuplicateFilesCommand = new("deleteduplicate")
    {
        new Option<string>("--dir")
        {
            IsRequired = true
        },
        new Option<bool>("--checkOnly")
        {
            IsRequired = false
        }
    };
    deleteDuplicateFilesCommand.Handler = CommandHandler.Create<string, bool?>((dir, checkOnly) => DeleteDuplicateFiles(dir, checkOnly ?? true));

    rootCommand.AddCommand(showDrivesCommand);
    rootCommand.AddCommand(specialFoldersCommand);
    rootCommand.AddCommand(createFileCommand);
    rootCommand.AddCommand(fileInfoCommand);
    rootCommand.AddCommand(changePropertiesCommand);
    rootCommand.AddCommand(readLinesCommand);
    rootCommand.AddCommand(writeFileCommand);
    rootCommand.AddCommand(deleteDuplicateFilesCommand);
    return new CommandLineBuilder(rootCommand);
}

void ShowDrivesInformation()
{
    DriveInfo[] drives = DriveInfo.GetDrives();
    foreach (DriveInfo drive in drives)
    {
        if (drive.IsReady)
        {
            Console.WriteLine($"Drive name: {drive.Name}");
            Console.WriteLine($"Format: {drive.DriveFormat}");
            Console.WriteLine($"Type: {drive.DriveType}");
            Console.WriteLine($"Root directory: {drive.RootDirectory}");
            Console.WriteLine($"Volume label: {drive.VolumeLabel}");
            Console.WriteLine($"Free space: {drive.TotalFreeSpace}");
            Console.WriteLine($"Available space: {drive.AvailableFreeSpace}");
            Console.WriteLine($"Total size: {drive.TotalSize}");

            Console.WriteLine();
        }
    }
}

void CreateFile(string file)
{
    try
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), file);
        File.WriteAllText(path, "Hello, World!");
        Console.WriteLine($"created file {path}");
    }
    catch (ArgumentException)
    {
        Console.WriteLine("Invalid characters in the filename?");
    }
}

void ShowSpecialFolders()
{
    foreach (var specialFolder in Enum.GetNames(typeof(Environment.SpecialFolder)))
    {
        Environment.SpecialFolder folder = Enum.Parse<Environment.SpecialFolder>(specialFolder);

        string path = Environment.GetFolderPath(folder);
        Console.WriteLine($"{specialFolder}: {path}");
    }
}

void FileInformation(string file)
{
    try
    {
        FileInfo fileInfo = new(file);
        Console.WriteLine($"Name: {fileInfo.Name}");
        Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
        Console.WriteLine($"Read only: {fileInfo.IsReadOnly}");
        Console.WriteLine($"Extension: {fileInfo.Extension}");
        Console.WriteLine($"Length: {fileInfo.Length}");
        Console.WriteLine($"Creation time: {fileInfo.CreationTime:F}");
        Console.WriteLine($"Access time: {fileInfo.LastAccessTime:F}");
        Console.WriteLine($"File attributes: {fileInfo.Attributes}");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("file not found");
    }
}

void ChangeFileProperties(string file)
{
    FileInfo fileInfo = new(file);
    if (!fileInfo.Exists)
    {
        Console.WriteLine($"File {file} does not exist");
        return;
    }

    Console.WriteLine($"creation time: {fileInfo.CreationTime:F}");
    fileInfo.CreationTime = new DateTime(2035, 12, 24, 15, 0, 0);
    Console.WriteLine($"creation time: {fileInfo.CreationTime:F}");
}

void ReadLineByLine(string file)
{
    IEnumerable<string> lines = File.ReadLines(file);
    int i = 1;
    foreach (var line in lines)
    {
        Console.WriteLine($"{i++}. {line}");
    }
}

void WriteAFile()
{
    string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "movies.txt");
    string[] movies =
    {
        "Snow White And The Seven Dwarfs",
        "Gone With The Wind",
        "Casablanca",
        "The Bridge On The River Kwai",
        "Some Like It Hot"
    };

    File.WriteAllLines(fileName, movies);

    string[] moreMovies =
    {
        "Psycho",
        "Easy Rider",
        "Pulp Fiction",
        "Star Wars",
        "The Matrix"
    };
    File.AppendAllLines(fileName, moreMovies);
}

void DeleteDuplicateFiles(string directory, bool checkOnly = true)
{
    IEnumerable<string> fileNames = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories);
    string previousFileName = string.Empty;
    foreach (string fileName in fileNames)
    {
        string previousName = Path.GetFileNameWithoutExtension(previousFileName);
        int ix = previousFileName.LastIndexOf(" - Copy");
        if (!string.IsNullOrEmpty(previousFileName) &&
            previousName.EndsWith(" - Copy") &&
            fileName.StartsWith(previousFileName[..ix]))
        {
            FileInfo copiedFile = new(previousFileName);
            FileInfo originalFile = new(fileName);
            if (copiedFile.Length == originalFile.Length)
            {
                Console.WriteLine($"delete {copiedFile.FullName}");
                if (!checkOnly)
                {
                    copiedFile.Delete();
                }
            }
        }
        previousFileName = fileName;
    }
}
