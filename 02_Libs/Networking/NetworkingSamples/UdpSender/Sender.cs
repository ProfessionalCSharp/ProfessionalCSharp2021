using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Sender
{
    private readonly ILogger _logger;
    public Sender(ILogger<Sender> logger) => _logger = logger;

    private async Task<IPEndPoint?> GetIPEndPointAsync(int port, string? hostName, bool broadcast, string? groupAddress, bool ipv6)
    {
        IPEndPoint? endpoint = null;
        try
        {
            if (broadcast)
            {
                endpoint = new IPEndPoint(IPAddress.Broadcast, port);
            }
            else if (hostName != null)
            {
                IPHostEntry hostEntry = await Dns.GetHostEntryAsync(hostName);
                IPAddress? address = null;
                if (ipv6)
                {
                    address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetworkV6).FirstOrDefault();
                }
                else
                {
                    address = hostEntry.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                }

                if (address == null)
                {
                    Func<string> ipversion = () => ipv6 ? "IPv6" : "IPv4";
                    _logger.LogWarning($"no {ipversion()} address for {hostName}");
                    return null;
                }
                endpoint = new IPEndPoint(address, port);
            }
            else if (groupAddress != null)
            {
                endpoint = new IPEndPoint(IPAddress.Parse(groupAddress), port);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(hostName)}, {nameof(broadcast)}, or {nameof(groupAddress)} must be set");
            }
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return endpoint;
    }

    public async Task RunAsync(int port, string? hostName, bool broadcast, string? groupAddress, bool ipv6)
    {
        IPEndPoint? endpoint = await GetIPEndPointAsync(port, hostName, broadcast, groupAddress, ipv6);
        if (endpoint == null) return;

        try
        {
            string localhost = Dns.GetHostName();
            using UdpClient client = new(); // (new IPEndPoint(IPAddress.Parse("192.168.178.20"), 0));
            client.EnableBroadcast = broadcast;
            if (groupAddress != null)
            {
                client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
            }

            bool completed = false;
            do
            {
                Console.WriteLine("Enter a message or bye to exit");
                string? input = Console.ReadLine();
                Console.WriteLine();
                completed = input == "bye";

                byte[] datagram = Encoding.UTF8.GetBytes($"{input} from {localhost}");
                int sent = await client.SendAsync(datagram, datagram.Length, endpoint);
                _logger.LogInformation($"Sent datagram using local EP {client.Client.LocalEndPoint} to {endpoint}");
            } while (!completed);

            if (groupAddress != null)
            {
                client.DropMulticastGroup(IPAddress.Parse(groupAddress));
            }
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
