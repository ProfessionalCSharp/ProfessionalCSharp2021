using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Receiver
{
    public async Task RunAsync(int port, string? groupAddress)
    {
        using UdpClient client = new(port);

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
            Console.WriteLine($"received: {received}");
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
}

