using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("MenusConnection");
        services.AddDbContext<MenusContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();
await runner.FindByKeyAsync(23);
await runner.SingleOrDefaultAsync("menu 27");
await runner.FirstOrDefaultAsync("menu 27");
await runner.WhereAsync();
await runner.PagingAsync(10, 5);
await runner.GetAllMenusUsingAsyncStream();
// await runner.RawSqlAsync("menu 27");
runner.UseCompiledQuery();
await runner.UseCompiledQueryAsync();
await runner.UseEFCunctions("24");

await runner.DeleteDatabaseAsync();


