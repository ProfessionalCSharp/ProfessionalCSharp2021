using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

record ReceiverOptions
{
    public int Port { get; set; }
    public string? GroupAddress { get; set; }
}

class Receiver
{
    private readonly ILogger _logger;
    private readonly int _port;
    private readonly string? _groupAddress;
    public Receiver(IOptions<ReceiverOptions> options, ILogger logger)
    {
        _port = options.Value.Port;
        _groupAddress = options.Value.GroupAddress;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        using UdpClient client = new(_port);

        if (_groupAddress != null)
        {
            client.JoinMulticastGroup(IPAddress.Parse(_groupAddress));
            _logger.LogInformation("joining the multicast group {0}", IPAddress.Parse(_groupAddress));
        }

        bool completed = false;
        do
        {
            _logger.LogInformation("Starting the receiver");
            UdpReceiveResult result = await client.ReceiveAsync();
            byte[] datagram = result.Buffer;
            string received = Encoding.UTF8.GetString(datagram);
            Console.WriteLine($"received: {received}");
            if (received == "bye")
            {
                completed = true;
            }
        } while (!completed);
        _logger.LogInformation("Receiver closing");

        if (_groupAddress != null)
        {
            client.DropMulticastGroup(IPAddress.Parse(_groupAddress));
        }
    }
}

