using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Sockets;
using System.Text;

public record ReceiverOptions
{
    public int Port { get; init; }
    public bool UseBroadcast { get; init; } = false;
    public string? GroupAddress { get; init; }
}

public class Receiver
{
    private readonly ILogger _logger;
    private readonly int _port;
    private readonly string? _groupAddress;
    private readonly bool _useBroadcast;
    public Receiver(IOptions<ReceiverOptions> options, ILogger<Receiver> logger)
    {
        _port = options.Value.Port;
        _groupAddress = options.Value.GroupAddress;
        _useBroadcast = options.Value.UseBroadcast;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        using UdpClient client = new(_port);
        client.EnableBroadcast = _useBroadcast;

        if (_groupAddress != null)
        {
            client.JoinMulticastGroup(IPAddress.Parse(_groupAddress));
            _logger.LogInformation("joining the multicast group {0}", IPAddress.Parse(_groupAddress));
        }

        bool completed = false;
        do
        {
            _logger.LogInformation("Waiting to receivd data");
            UdpReceiveResult result = await client.ReceiveAsync();
            byte[] datagram = result.Buffer;
            string dataReceived = Encoding.UTF8.GetString(datagram);
            _logger.LogInformation("Received {0} from {1}", dataReceived, result.RemoteEndPoint);
            if (dataReceived.Equals("bye", StringComparison.CurrentCultureIgnoreCase))
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

