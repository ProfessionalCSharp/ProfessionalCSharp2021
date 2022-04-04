# Readme - Code Samples for Chapter 18, Files and Streams

**Files and Streams** not only covers reading and writing from the file system with new stream APIs that allow using the Span type but also covers the new .NET JSON serializer with classes in the `System.Text.Json` namespace.

This chapter contains the following code samples:

* FilesAndFolders (using `DriveInfo`, `Path`)

* Working with Files and Folders (accessing files and directories using `File`, `Directory`, and `Path`)
* Stream Samples (`FileStream`, `StreamReader`, `StreamWriter`, `Encoding`)
* Reader Writer Samples (reading and writing binary and text files)
* Compress File Sample (compressing and uncompressing using `DeflateStream`, `ZipArchive`, and `BrotliStream`)
* File Monitor (monitoring file changes with `FileSystemWatcher`)
* JsonSample (JSON serialization, reader, writer, DOM access)
* WindowsAppEditor (an editor using WinUI)

## Updates with C# 10 and .NET 6

.NET 6 contains a new API for writing JSON via DOM. See the updated sample code [JSON](../../5_More/FilesAndStreams/JsonSample/).

Also see [Updates with C# 10](../../Dotnet6Updates.md)

## WinUI

The WindowsAppEditor sample needs to have WinUI installed. See [WinUI](../../WinUI.md) for information on installing and using the WinUI samples.

The sample code uses a picker which currently (Project Reunion 0.5.7) needs to set the active window. See the `InitializeActiveWindow` method in `MainWindow.xaml.cs`. This will change with a future version and will be updated here.

## More Information
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com) for additional information for topics covered in the book.

Thank you!