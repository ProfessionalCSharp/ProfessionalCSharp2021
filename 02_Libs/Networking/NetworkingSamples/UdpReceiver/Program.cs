using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

await BuildCommandLine()
    .UseHost(_ => Host.CreateDefaultBuilder(), hostBuilder =>
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddTransient<Receiver>();
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
 
    rootCommand.AddOption(portOption);
    rootCommand.AddOption(groupAddressOption);
    rootCommand.Handler = CommandHandler.Create<int, string?, IHost>(RunAsync);
    CommandLineBuilder commandLineBuilder = new(rootCommand);
 
    return commandLineBuilder;
}

async Task RunAsync(int port,string? groupAddress, IHost host)
{
    var receiver = host.Services.GetRequiredService<Receiver>();
    await receiver.RunAsync(port, groupAddress);
}

