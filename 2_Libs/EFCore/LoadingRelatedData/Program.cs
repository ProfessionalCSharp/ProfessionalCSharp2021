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

using (var scope = host.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();

    await runner.CreateTheDatabaseAsync();
    
    await runner.EagerLoadingAsync();
    await runner.FilteredIncludeAsync();
}

using (var scope = host.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();
    await runner.ExplicitLoadingAsync();
}

using (var scope = host.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<Runner>();
    await runner.LazyLoadingAsync();

    await runner.DeleteDatabaseAsync();
}

