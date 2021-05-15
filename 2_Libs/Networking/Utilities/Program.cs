using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Net;

await BuildCommandLine()
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

CommandLineBuilder BuildCommandLine()
{
    RootCommand rootCommand = new("Utilities");
    Command uriCommand = new("uri", "Show the parts of the URI, e.g. www.wrox.com")
    {
        new Option<string>("--uri")
        {
            IsRequired = true
        }
    };
    uriCommand.Handler = CommandHandler.Create<string>(UriSample);
    Command ipCommand = new("ip", "Show the part of the IP address, e.g. ipaddress 234.56.78.9")
    {
        new Option<string>("--ip")
        {
            IsRequired = true
        }
    };
    ipCommand.Handler = CommandHandler.Create<string>(ip => IPAddressSample(ip));
    Command buildUriCommand = new("builduri", "Build an URI using the UriBuilder")
    {
        Handler = CommandHandler.Create(BuildUri)
    };

    rootCommand.AddCommand(uriCommand);
    rootCommand.AddCommand(ipCommand);
    rootCommand.AddCommand(buildUriCommand);
    return new CommandLineBuilder(rootCommand);
}

void IPAddressSample(string ipAddressString)
{
    if (!IPAddress.TryParse(ipAddressString, out IPAddress? address))
    {
        Console.WriteLine($"cannot parse {ipAddressString}");
        return;
    }

    byte[] bytes = address.GetAddressBytes();
    for (int i = 0; i < bytes.Length; i++)
    {
        Console.WriteLine($"byte {i}: {bytes[i]:X}");
    }

    Console.WriteLine($"family: {address.AddressFamily}, map to ipv6: {address.MapToIPv6()}, map to ipv4: {address.MapToIPv4()}");
    Console.WriteLine($"IPv4 loopback address: {IPAddress.Loopback}");
    Console.WriteLine($"IPv6 loopback address: {IPAddress.IPv6Loopback}");
    Console.WriteLine($"IPv4 broadcast address: {IPAddress.Broadcast}");
    Console.WriteLine($"IPv4 any address: {IPAddress.Any}");
    Console.WriteLine($"IPv6 any address: {IPAddress.IPv6Any}");
}

void UriSample(string uri)
{
    Uri page = new(uri);
    Console.WriteLine($"scheme: {page.Scheme}");

    Console.WriteLine($"host: {page.Host}, type:  {page.HostNameType}, idn host: {page.IdnHost}");
    Console.WriteLine($"port: {page.Port}");
    Console.WriteLine($"path: {page.AbsolutePath}");
    Console.WriteLine($"query: {page.Query}");
    foreach (var segment in page.Segments)
    {
        Console.WriteLine($"segment: {segment}");
    }
}

void BuildUri()
{
    UriBuilder builder = new();
    builder.Scheme = "https";
    builder.Host = "www.cninnovation.com";
    builder.Port = 443;
    builder.Path = "training/MVC";
    Uri uri = builder.Uri;
    Console.WriteLine(uri);
}
