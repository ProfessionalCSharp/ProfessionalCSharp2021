using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

string? _pipeHandle;
ManualResetEventSlim _pipeHandleSet = new(initialState: false);

Task.Run(() => Reader());
Task.Run(() => Writer());

void Writer()
{
    Console.WriteLine("anonymous pipe writer");
    _pipeHandleSet.Wait();
    if (_pipeHandle is null) throw new InvalidOperationException(); // should not be null after signalled

    AnonymousPipeClientStream pipeWriter = new(PipeDirection.Out, _pipeHandle);
    using StreamWriter writer = new(pipeWriter);

    writer.AutoFlush = true;
    Console.WriteLine("starting writer");
    for (int i = 0; i < 5; i++)
    {
        writer.WriteLine($"Message {i}");
        Task.Delay(500).Wait();
    }
    writer.WriteLine("end");
}

void Reader()
{
    try
    {
        Console.WriteLine("anonymous pipe reader");
        var pipeReader = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.None);
        using var reader = new StreamReader(pipeReader);

        _pipeHandle = pipeReader.GetClientHandleAsString();
        Console.WriteLine($"pipe handle: {_pipeHandle}");
        _pipeHandleSet.Set();
        bool end = false;
        while (!end)
        {
            string? line = reader.ReadLine();
            Console.WriteLine(line);
            if (line == "end") end = true;
        }
        Console.WriteLine("finished reading");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
