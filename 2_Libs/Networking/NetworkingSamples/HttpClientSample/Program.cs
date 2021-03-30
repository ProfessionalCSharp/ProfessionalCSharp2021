using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Net;
using System.Net.Http;
using System.Threading;

await BuildCommandLine()
    .UseHost(_ => GetHostBuilder())
    .UseDefaults()
    .Build()
    .InvokeAsync(args);

IHostBuilder GetHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            var httpClientSettings = context.Configuration.GetSection("HttpClient");
            services.Configure<HttpClientSamplesOptions>(httpClientSettings);
            services.AddHttpClient<HttpClientSamples>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings["Url"]);
            });

            services.Configure<LimitCallsHandlerOptions>(context.Configuration.GetSection("RateLimit"));
            services.AddTransient<LimitCallsHandler>();
            services.AddHttpClient<HttpClientSampleWithMessageHandler>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings["Url"]);
            }).AddHttpMessageHandler<LimitCallsHandler>().SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.AddHttpClient("racersClient")
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(httpClientSettings["Url"]);
                });
            services.AddTransient<NamedClientSample>();

            services.AddHttpClient<FaultHandlingSample>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings["InvalidUrl"]);
            })            
            //.AddPolicyHandler(GetRetryPolicy())
            .AddTransientHttpErrorPolicy(
                policy => policy.WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5) }));
        });

IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    => HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(message => message.StatusCode == HttpStatusCode.TooManyRequests)
        .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt)));

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

    Command headerCommand = new("headers");
    headerCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.AddHttpHeadersAsync();
    });
    rootCommand.AddCommand(headerCommand);

    Command http2Command = new("http2");
    http2Command.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSamples>();
        await service.UseHttp2();
    });
    rootCommand.AddCommand(http2Command);

    Command messageHandlerCommand = new("messagehandler");
    messageHandlerCommand.Handler = CommandHandler.Create<IHost>(async (host) =>
    {
        var service = host.Services.GetRequiredService<HttpClientSampleWithMessageHandler>();
        for (int i = 0; i < 10; i++)
        {
            await service.UseMessageHandlerAsync();
        }

    });
    rootCommand.AddCommand(messageHandlerCommand);

    Command namedClientCommand = new("named");
    namedClientCommand.Handler = CommandHandler.Create<IHost>(async host =>
    {
        var service = host.Services.GetRequiredService<NamedClientSample>();
        await service.RunAsync();
    });
    rootCommand.AddCommand(namedClientCommand);

    Command pollyCommand = new("retry");
    pollyCommand.Handler = CommandHandler.Create<IHost>(async host =>
    {
        var service = host.Services.GetRequiredService<FaultHandlingSample>();
        await service.RunAsync();
    });
    rootCommand.AddCommand(pollyCommand);

    return new CommandLineBuilder(rootCommand);
}
