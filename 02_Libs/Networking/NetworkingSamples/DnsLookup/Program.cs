using System;
using System.Net;
using System.Threading.Tasks;

do
{
    Console.Write("Hostname:\t");
    string? hostname = Console.ReadLine();
    if (hostname is null || hostname.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine("bye!");
        return;
    }
    await OnLookupAsync(hostname);
    Console.WriteLine();
} while (true);


async Task OnLookupAsync(string hostname)
{
    try
    {
        IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostname);

        Console.WriteLine($"Hostname: {ipHost.HostName}");

        // Aliases not populated by GetHostEntryAsync
        //if (ipHost.Aliases.Length != 0)
        //{
        //    Console.WriteLine($"Aliases: {string.Join(", ", ipHost.Aliases)}");
        //}

        foreach (IPAddress address in ipHost.AddressList)
        {
            Console.WriteLine($"Address Family: {address.AddressFamily}, address: {address}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
