using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

await BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(), hostBuilder =>
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddTransient<Sender>();
        });
    })
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
    var broadcastFlag = new Option<bool>("--broadcast")
    {
        IsRequired = false
    };
    broadcastFlag.AddAlias("-b");
    var hostNameOption = new Option<string>("--hostname", "Use the hostname if the message should be sent to a single host")
    {
        IsRequired = false
    };
    hostNameOption.AddAlias("-h");
    var ipv6Flag = new Option<bool>("--ipv6")
    {
        IsRequired = false
    };
    rootCommand.AddOption(portOption);
    rootCommand.AddOption(groupAddressOption);
    rootCommand.AddOption(broadcastFlag);
    rootCommand.AddOption(hostNameOption);
    rootCommand.AddOption(ipv6Flag);
    rootCommand.Handler = CommandHandler.Create<int, string?, bool, string?, bool, IHost>(RunAsync);
    CommandLineBuilder commandLineBuilder = new(rootCommand);
    commandLineBuilder.UseMiddleware(async (context, next) =>
    {
        context.Console.Out.WriteLine("my middle");
        if (context.ParseResult.HasOption(hostNameOption) || context.ParseResult.HasOption(broadcastFlag) || context.ParseResult.HasOption(groupAddressOption))
        {
            await next(context);
        }
        else
        {
            context.Console.Out.WriteLine("Supply hostname, groupaddress or broadcast");
        }
    });
    return commandLineBuilder;
}

async Task RunAsync(int port, string? hostName, bool broadcast, string? groupAddress, bool ipv6, IHost host)
{
    var sender = host.Services.GetRequiredService<Sender>();
    await sender.RunAsync(port, hostName, broadcast, groupAddress, ipv6);
}
