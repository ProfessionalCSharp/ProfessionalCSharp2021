using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Net.Http;

await BuildCommandLine()
    .UseHost(_ =>
    {
        return Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            var httpClientSettings = context.Configuration.GetSection("HttpClient");
            services.Configure<HttpClientSamplesOptions>(httpClientSettings);
            services.AddHttpClient<HttpClientSamples>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings["Url"]);
            });
            services.AddHttpClient<HttpClientSampleWithMessageHandler>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings["Url"]);
            }).ConfigurePrimaryHttpMessageHandler<SampleMessageHandler>();

        });
    })
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

CommandLineBuilder BuildCommandLine()
{
    RootCommand rootCommand = new("HttpClientSample");
    Command simpleCommand = new("simple");
    simpleCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.SimpleGetRequestAsync();
    });
    rootCommand.AddCommand(simpleCommand);

    Command httpRequestMessageCommand = new("httprequest");
    httpRequestMessageCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.UseHttpRequestMessageAsync();
    });
    rootCommand.AddCommand(httpRequestMessageCommand);

    Command exceptionCommand = new("exception");
    exceptionCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.ThrowExceptionAsync();
    });
    rootCommand.AddCommand(exceptionCommand);

    Command headerCommand = new("header");
    headerCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.AddHttpHeadersAsync();
    });
    rootCommand.AddCommand(headerCommand);

    Command messageHandlerCommand = new("messagehandler");
    messageHandlerCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSampleWithMessageHandler>();
        await service.UseMessageHandlerAsync();
    });
    rootCommand.AddCommand(messageHandlerCommand);

    return new CommandLineBuilder(rootCommand);
}
