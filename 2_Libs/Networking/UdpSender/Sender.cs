using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public record SenderOptions
{
    public int ReceiverPort { get; init; }
    public string? HostName { get; init; }
    public bool UseBroadcast { get; init; } = false;
    public string? GroupAddress { get; init; }
    public bool UseIpv6 { get; init; } = false;
}

public class Sender
{
    private readonly ILogger _logger;
    private readonly int _port;
    private readonly string? _hostName;
    private readonly bool _useBroadcast;
    private readonly string? _groupAddress;
    private readonly bool _useIpv6;
    public Sender(IOptions<SenderOptions> options, ILogger<Sender> logger)
    {
        _logger = logger;
        _port = options.Value.ReceiverPort;
        _hostName = options.Value.HostName;
        _useBroadcast = options.Value.UseBroadcast;
        _groupAddress = options.Value.GroupAddress;
        _useIpv6 = options.Value.UseIpv6;
    }

    private async Task<IPEndPoint?> GetReceiverIPEndPointAsync()
    {
        IPEndPoint? endpoint = null;
        try
        {
            if (_useBroadcast)
            {
                endpoint = new IPEndPoint(IPAddress.Broadcast, _port);
            }
            else if (_hostName != null)
            {
                IPHostEntry hostEntry = await Dns.GetHostEntryAsync(_hostName);
                IPAddress? address = null;
                if (_useIpv6)
                {
                    address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetworkV6).FirstOrDefault();
                }
                else
                {
                    address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                }

                if (address == null)
                {
                    Func<string> ipversion = () => _useIpv6 ? "IPv6" : "IPv4";
                    _logger.LogWarning($"no {ipversion()} address for {_hostName}");
                    return null;
                }
                endpoint = new IPEndPoint(address, _port);
            }
            else if (_groupAddress != null)
            {
                endpoint = new IPEndPoint(IPAddress.Parse(_groupAddress), _port);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(_hostName)}, {nameof(_useBroadcast)}, or {nameof(_groupAddress)} must be set");
            }
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return endpoint;
    }

    public async Task RunAsync()
    {
        IPEndPoint? endpoint = await GetReceiverIPEndPointAsync();
        if (endpoint is null) return;

        try
        {
            string localhost = Dns.GetHostName();
            using UdpClient client = new();
            client.EnableBroadcast = _useBroadcast;
            if (_groupAddress != null)
            {
                client.JoinMulticastGroup(IPAddress.Parse(_groupAddress));
            }

            bool completed = false;
            do
            {
                Console.WriteLine(@$"{Environment.NewLine}Enter a message or ""bye"" to exit");
                string? input = Console.ReadLine();
                if (input is null) continue;
                completed = input.Equals("bye", StringComparison.CurrentCultureIgnoreCase);

                byte[] datagram = Encoding.UTF8.GetBytes(input);
                int sent = await client.SendAsync(datagram, datagram.Length, endpoint);
                _logger.LogInformation("Sent datagram using local EP {0} to {1}", client.Client.LocalEndPoint, endpoint);
            } while (!completed);

            if (_groupAddress != null)
            {
                client.DropMulticastGroup(IPAddress.Parse(_groupAddress));
            }
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
