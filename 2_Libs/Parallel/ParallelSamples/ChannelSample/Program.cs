using System.Threading.Channels;

await UsingTheUnboundedChannelAsync();
await UsingTheBoundedChannelAsync();

async Task UsingTheUnboundedChannelAsync()
{
    Channel<SomeData> channel = Channel.CreateUnbounded<SomeData>(new UnboundedChannelOptions() { SingleReader = false, SingleWriter = true, });

    Console.WriteLine("Using the unbounded channel");

    var t1 = ChannelSample.WriteSomeDataAsync(channel.Writer);
    var t2 = ChannelSample.ReadSomeDataAsync(channel.Reader);

    await Task.WhenAll(t1, t2);

    Console.WriteLine();
}

async Task UsingTheBoundedChannelAsync()
{
    Channel<SomeData> channel = Channel.CreateBounded<SomeData>(new BoundedChannelOptions(capacity: 10) { FullMode = BoundedChannelFullMode.Wait, SingleWriter = true });

    Console.WriteLine("Using the bounded channel");

    var t1 = ChannelSample.WriteSomeDataWithTryWriteAsync(channel.Writer);
    var t2 = ChannelSample.ReadSomeDataUsingAsyncStreams(channel.Reader);

    await Task.WhenAll(t1, t2);

    Console.WriteLine("bye");
    Console.WriteLine();
}


public record SomeData(string Text, int Number);
