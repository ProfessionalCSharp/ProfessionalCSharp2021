using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

string SampleDataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "samplefile.data");

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
    fileOption.AddAlias("-f");
    Option<string> inputFileOption = new("--input")
    {
        IsRequired = true
    };
    inputFileOption.AddAlias("-i");
    Option<string> outputFileOption = new("--output")
    {
        IsRequired = true
    };
    outputFileOption.AddAlias("-o");
    RootCommand rootCommand = new("WorkingWithFilesAndDirectories");
    Command readWithStreamCommand = new("readwithstream") { fileOption };
    readWithStreamCommand.Handler = CommandHandler.Create<string>(ReadUsingFileStream);
    Command writeTextFileCommand = new("writetext") { Handler = CommandHandler.Create(WriteTextFile) };
    Command copyCommand = new("copy") { inputFileOption, outputFileOption };
    copyCommand.Handler = CommandHandler.Create<string, string>(CopyUsingStreams);
    Command copy2Command = new("copy2") { inputFileOption, outputFileOption };
    copy2Command.Handler = CommandHandler.Create<string, string>(CopyUsingStreams2);
    Command createSampleFile = new("samplefile")
    {
        new Option<int>("--count")
        {
            IsRequired = true
        }
    };
    createSampleFile.Handler = CommandHandler.Create<int>(CreateSampleFileAsync);
    Command randomAccessCommand = new("random") { Handler = CommandHandler.Create(RandomAccessSampleAsync) };

    rootCommand.AddCommand(readWithStreamCommand);
    rootCommand.AddCommand(writeTextFileCommand);
    rootCommand.AddCommand(copyCommand);
    rootCommand.AddCommand(copy2Command);
    rootCommand.AddCommand(createSampleFile);
    rootCommand.AddCommand(randomAccessCommand);

    return new CommandLineBuilder(rootCommand);
}

void ReadUsingFileStream(string fileName)
{
    const int BUFFERSIZE = 4096;
    using FileStream stream = new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

    ShowStreamInformation(stream);
    Encoding encoding = GetEncoding(stream);

    var buffer = new byte[BUFFERSIZE].AsSpan();

    bool completed = false;
    do
    {
        int nread = stream.Read(buffer);
        if (nread == 0) completed = true;
        if (nread < buffer.Length)
        {
            buffer[nread..].Clear();
        }

        string s = encoding.GetString(buffer[..nread]);
        Console.WriteLine($"read {nread} bytes");
        Console.WriteLine(s);
    } while (!completed);
}

void ShowStreamInformation(Stream stream)
{
    Console.WriteLine($"stream can read: {stream.CanRead}, can write: {stream.CanWrite}, can seek: {stream.CanSeek}, can timeout: {stream.CanTimeout}");
    Console.WriteLine($"length: {stream.Length}, position: {stream.Position}");
    if (stream.CanTimeout)
    {
        Console.WriteLine($"read timeout: {stream.ReadTimeout} write timeout: {stream.WriteTimeout} ");
    }
}

// read BOM
Encoding GetEncoding(Stream stream)
{
    if (!stream.CanSeek) throw new ArgumentException("require a stream that can seek");

    Encoding encoding = Encoding.ASCII;

    var bom = new byte[5].AsSpan();
    int nRead = stream.Read(bom);
    if (bom[0] is 0xff && bom[1] is 0xfe && bom[2] is 0 && bom[3] is 0)
    {
        Console.WriteLine("UTF-32");
        stream.Seek(4, SeekOrigin.Begin);
        return Encoding.UTF32;
    }
    else if (bom[0] == 0xff && bom[1] == 0xfe)
    {
        Console.WriteLine("UTF-16, little endian");
        stream.Seek(2, SeekOrigin.Begin);
        return Encoding.Unicode;
    }
    else if (bom[0] == 0xfe && bom[1] == 0xff)
    {
        Console.WriteLine("UTF-16, big endian");
        stream.Seek(2, SeekOrigin.Begin);
        return Encoding.BigEndianUnicode;
    }
    else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
    {
        Console.WriteLine("UTF-8");
        stream.Seek(3, SeekOrigin.Begin);
        return Encoding.UTF8;
    }
    stream.Seek(0, SeekOrigin.Begin);
    return encoding;
}

void WriteTextFile()
{
    string tempTextFileName = Path.ChangeExtension(Path.GetTempFileName(), "txt");
    using FileStream stream = File.OpenWrite(tempTextFileName);

    //// write BOM
    //stream.WriteByte(0xef);
    //stream.WriteByte(0xbb);
    //stream.WriteByte(0xbf);

    var preamble = Encoding.UTF8.GetPreamble().AsSpan();
    stream.Write(preamble);

    string hello = "Hello, World!";
    var buffer = Encoding.UTF8.GetBytes(hello).AsSpan();
    stream.Write(buffer);
    Console.WriteLine($"file {stream.Name} written");
}

void CopyUsingStreams(string inputFile, string outputFile)
{
    const int BUFFERSIZE = 4096;
    using var inputStream = File.OpenRead(inputFile);
    using var outputStream = File.OpenWrite(outputFile);

    var buffer = new byte[BUFFERSIZE].AsSpan();

    bool completed = false;
    do
    {
        int nRead = inputStream.Read(buffer);
        if (nRead == 0) completed = true;
        outputStream.Write(buffer[..nRead]);
    } while (!completed);

}

void CopyUsingStreams2(string inputFile, string outputFile)
{
    using var inputStream = File.OpenRead(inputFile);
    using var outputStream = File.OpenWrite(outputFile);

    inputStream.CopyTo(outputStream);
}

async Task CreateSampleFileAsync(int count)
{
    FileStream stream = File.Create(SampleDataFilePath);
    using StreamWriter writer = new(stream);

    Random r = new();
    var records = Enumerable.Range(1, count).Select(x => 
    (
        Number: x,
        Text: $"Sample text {r.Next(200)}",
        Date: new DateTime(Math.Abs((long)((r.NextDouble() * 2 - 1) * DateTime.MaxValue.Ticks)))
    ));
    Console.WriteLine("Start writing records...");
    foreach (var rec in records)
    {
        string date = rec.Date.ToString("d", CultureInfo.InvariantCulture);
        string s = $"#{rec.Number,8};{rec.Text,-20};{date}#{Environment.NewLine}";
        await writer.WriteAsync(s);
    }
    Console.WriteLine($"Created the file {SampleDataFilePath}");
}

async Task RandomAccessSampleAsync()
{
    const int RECORDSIZE = 44;

    try
    {
        using FileStream stream = File.OpenRead(SampleDataFilePath);
        var buffer = new byte[RECORDSIZE].AsMemory();

        do
        {
            try
            {
                Console.Write("record number (or 'bye' to end): ");
                string line = Console.ReadLine() ?? throw new InvalidOperationException();
                if (string.Equals(line, "bye", StringComparison.CurrentCultureIgnoreCase)) break;

                if (int.TryParse(line, out int record))
                {
                    stream.Seek((record - 1) * RECORDSIZE, SeekOrigin.Begin);
                    int read = await stream.ReadAsync(buffer);                    
                    string s = Encoding.UTF8.GetString(buffer.Span[0..read]);
                    Console.WriteLine($"record: {s}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (true);
        Console.WriteLine("finished");

    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Create the sample file using the option -sample first");
    }
}
