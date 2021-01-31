using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.CommandLine.Hosting;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

await BuildCommandLine()
    .UseHost(_ =>
    {
        return Host.CreateDefaultBuilder()
        .ConfigureServices(services =>
        {
            services.AddHttpClient<HttpClientSamples>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:5020");
            });
        });
    })
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

Console.ReadLine();

CommandLineBuilder BuildCommandLine()
{
    RootCommand rootCommand = new("HttpClientSample");
    Command simpleCommand = new("simple");
    simpleCommand.Handler = CommandHandler.Create<HttpClientSamples>(async (httpClientSample) =>
    {
        await httpClientSample.SimpleGetRequestAsync();
    });
    rootCommand.AddCommand(simpleCommand);

    return new CommandLineBuilder(rootCommand);
}



//class Program
//{
//    private static Command[]? s_Commands;

//    static async Task Main(string[] args)
//    {
//        HttpClientSamples samples = new();
//        s_Commands = SetupCommands(samples);

//        if (args.Length == 0 || args.Length > 1 || !s_Commands.Select(c => c.Option).Contains(args[0]))
//        {
//            ShowUsage();
//            return;
//        }

//        await s_Commands.Single(c => c.Option == args[0]).ActionAsync();
//        Console.ReadLine();
//    }

//    private static Command[] SetupCommands(HttpClientSamples samples) =>
//        new Command[]
//        {
//                new Command("-s", nameof(HttpClientSamples.GetDataSimpleAsync), samples.GetDataSimpleAsync),
//                new Command("-a", nameof(HttpClientSamples.GetDataAdvancedAsync), samples.GetDataAdvancedAsync),
//                new Command("-e", nameof(HttpClientSamples.GetDataWithExceptionsAsync), samples.GetDataWithExceptionsAsync),
//                new Command("-h", nameof(HttpClientSamples.GetDataWithHeadersAsync), samples.GetDataWithHeadersAsync),
//                new Command("-m", nameof(HttpClientSamples.GetDataWithMessageHandlerAsync), samples.GetDataWithMessageHandlerAsync),
//        };


//}
