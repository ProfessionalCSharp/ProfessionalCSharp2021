using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("TPHConnection");
        services.AddDbContext<BankContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();

await runner.CreateDatabaseAsync();
await runner.AddSampleDataAsync();
runner.QuerySample();
await runner.DeleteDatabaseAsync();
