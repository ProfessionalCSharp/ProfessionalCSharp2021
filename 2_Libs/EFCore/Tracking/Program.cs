﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("MenusConnection");
        services.AddDbContextFactory<MenusContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddRecordsAsync();
await runner.ObjectTrackingAsync();
await runner.UpdateRecordsAsync();
await runner.UpdateRecordUntrackedAsync();
await runner.EfficientUpdateAsync();

await runner.DeleteDatabaseAsync();
