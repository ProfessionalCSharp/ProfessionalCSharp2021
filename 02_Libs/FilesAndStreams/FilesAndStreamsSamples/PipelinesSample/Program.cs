using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

if (args == null || args.Length != 1)
{
    Console.WriteLine("please enter a directory");
    return;
}
string path = args[0];



Pipe pipe = new();
Task tWriter = WriteAsync(path, pipe.Writer);
// ReadAsync(path, pipe.Reader);

async Task ReadFromFiles(string path, CancellationToken token = default)
{
    foreach (var file in Directory.EnumerateFiles(path, "*.cs"))
    {
        using var stream = File.OpenRead(file);
        int bytesRead = await stream.ReadAsync(buffer, token);

        await writer.WriteAsync(buffer[..bytesRead], token);

    }
}

async Task WriteAsync(string path, PipeWriter writer, CancellationToken token = default)
{
    Memory<byte> buffer = new byte[8192].AsMemory();
    foreach (var file in Directory.EnumerateFiles(path, "*.cs"))
    {
        using var stream = File.OpenRead(file);
        int bytesRead = await stream.ReadAsync(buffer, token);

        await writer.WriteAsync(buffer[..bytesRead], token);
        
    }
}
