using System;
using System.Threading.Channels;
using System.Threading.Tasks;

class ChannelSample
{
    public static Task WriteSomeDataWithTryWriteAsync(ChannelWriter<SomeData> writer) =>
        Task.Run(async () =>
        {
            for (int i = 0; i < 100; i++)
            {
                Random r = new();
                SomeData data = new($"text {i}", i);
                await Task.Delay(r.Next(50));
                if (!writer.TryWrite(data))
                {
                    Console.WriteLine($"could not write {data.Number}, channel full");
                }
                else
                {
                    Console.WriteLine($"Written {data.Text}");
                }
            }
            writer.Complete();
            Console.WriteLine("Writing completed");
        });

    public static Task ReadSomeDataAsync(ChannelReader<SomeData> reader) =>
        Task.Run(async () =>
        {
            try
            {
                Console.WriteLine("Start reading...");
                Random r = new();
                while (true)
                {
                    await Task.Delay(r.Next(80));
                    var data = await reader.ReadAsync();
                    Console.WriteLine($"read: {data.Text}, available items: {reader.Count}");
                }
            }
            catch (ChannelClosedException)
            {
                Console.WriteLine("channel closed");
            }
        });
    

    public static Task WriteSomeDataAsync(ChannelWriter<SomeData> writer) =>
        Task.Run(async () =>
        {
            for (int i = 0; i < 100; i++)
            {
                Random r = new();
                SomeData data = new($"text {i}", i);
                await Task.Delay(r.Next(50));
                await writer.WriteAsync(data);
                Console.WriteLine($"Written {data.Text}");
            }
            writer.Complete();
            Console.WriteLine("Writing completed");
        });    

    public static Task ReadSomeDataUsingAsyncStreams(ChannelReader<SomeData> reader) =>    
        Task.Run(async () =>
        {
            try
            {
                Console.WriteLine("Start reading...");
                Random r = new();
                await foreach (var data in reader.ReadAllAsync())
                {
                    await Task.Delay(r.Next(80));
                    Console.WriteLine($"read: {data.Text} available items: {reader.Count}");
                }

            }
            catch (ChannelClosedException)
            {
                Console.WriteLine("channel closed");
            }
        });
    
}
