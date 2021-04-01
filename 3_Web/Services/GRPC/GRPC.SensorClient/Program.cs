using GRPCService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddGrpcClient<Sensor.SensorClient>(options =>
        {
            string grpcServiceUri = context.Configuration["GrpcServiceUri"] ?? "https://localhost:5001";
            options.Address = new Uri(grpcServiceUri);
            options.ChannelOptionsActions.Add(options =>
            {
                options.ThrowOperationCanceledOnCancellation = true;
            });
        });

        services.AddSingleton<Runner>();
    })
    .Build();

Console.WriteLine("press return to start");
Console.ReadLine();

var runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();

Console.ReadLine();
