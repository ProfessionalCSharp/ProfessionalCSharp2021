ReadFileUsingReader("./Program.cs");
Console.WriteLine();
string textFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "txt");
WriteFileUsingWriter(textFile, new string[] { "one", "two" });
Console.WriteLine($"Written temp file {textFile}");

string binFile = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), "bin");
Console.WriteLine($"writing to {binFile}");
WriteFileUsingBinaryWriter(binFile);
Console.WriteLine($"written to {binFile}");
ReadFileUsingBinaryReader(binFile);

void WriteFileUsingWriter(string fileName, string[] lines)
{
    var outputStream = File.OpenWrite(fileName);
    using StreamWriter writer = new(outputStream, Encoding.UTF8);
    
    var preamble = Encoding.UTF8.GetPreamble().AsSpan();
    outputStream.Write(preamble);
    foreach (var line in lines)
    {
        writer.WriteLine(line);
    }
}

void WriteFileUsingBinaryWriter(string binFile)
{
    var outputStream = File.Create(binFile);
    using BinaryWriter writer = new(outputStream);

    double d = 47.47;
    int i = 42;
    long l = 987654321;
    string s = "sample";
    writer.Write(d);
    writer.Write(i);
    writer.Write(l);
    writer.Write(s);
}

void ReadFileUsingBinaryReader(string binFile)
{
    var inputStream = File.Open(binFile, FileMode.Open);
    using BinaryReader reader = new(inputStream);

    double d = reader.ReadDouble();

    int i = reader.ReadInt32();
    long l = reader.ReadInt64();
    string s = reader.ReadString();
    Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
}

void ReadFileUsingReader(string fileName)
{
    FileStream stream = new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
    using StreamReader reader = new(stream);

    while (!reader.EndOfStream)
    {
        string? line = reader.ReadLine();
        Console.WriteLine(line);
    }
}