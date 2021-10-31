using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseLazyLoadingProxies(false);
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<Runner>();
    })
    .Build();

await using (var scope = host.Services.CreateAsyncScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();

    await runner.CreateTheDatabaseAsync();
    
    await runner.EagerLoadingAsync();
    await runner.FilteredIncludeAsync();
}

await using (var scope = host.Services.CreateAsyncScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();
    await runner.ExplicitLoadingAsync();
}

await using (var scope = host.Services.CreateAsyncScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();
    await runner.LazyLoadingAsync();

    await runner.DeleteDatabaseAsync();
}

