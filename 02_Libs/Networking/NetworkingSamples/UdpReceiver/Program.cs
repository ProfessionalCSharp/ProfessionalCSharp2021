using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

await BuildCommandLine()
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

CommandLineBuilder BuildCommandLine()
{
    RootCommand rootCommand = new("UdpSender");
    var portOption = new Option<int>("--port", "Port number, enter a port number of the receiver")
    {
        IsRequired = true
    };
    portOption.AddAlias("-p");
    var groupAddressOption = new Option<string>("--groupaddress", "Enter a group address in the range 224.0.0.0 to 239.255.255.255")
    {
        IsRequired = false
    };
    groupAddressOption.AddAlias("-g");
 
    rootCommand.AddOption(portOption);
    rootCommand.AddOption(groupAddressOption);
    rootCommand.Handler = CommandHandler.Create<int, string?>(ReaderAsync);
    CommandLineBuilder commandLineBuilder = new(rootCommand);
 
    return commandLineBuilder;
}

async Task ReaderAsync(int port, string? groupAddress)
{
    using var client = new UdpClient(port);

    if (groupAddress != null)
    {
        client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
        Console.WriteLine($"joining the multicast group {IPAddress.Parse(groupAddress)}");
    }

    bool completed = false;
    do
    {
        Console.WriteLine("starting the receiver");
        UdpReceiveResult result = await client.ReceiveAsync();
        byte[] datagram = result.Buffer;
        string received = Encoding.UTF8.GetString(datagram);
        Console.WriteLine($"received {received}");
        if (received == "bye")
        {
            completed = true;
        }
    } while (!completed);
    Console.WriteLine("receiver closing");

    if (groupAddress != null)
    {
        client.DropMulticastGroup(IPAddress.Parse(groupAddress));
    }
}
