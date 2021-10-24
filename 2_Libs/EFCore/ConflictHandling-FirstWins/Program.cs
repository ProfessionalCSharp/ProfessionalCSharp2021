using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<Runner>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var creator = scope.ServiceProvider.GetRequiredService<Runner>();

    await creator.CreateTheDatabaseAsync();
}

// different scopes for the users and prepare update
using var user1Scope = host.Services.CreateScope();
using var user2Scope = host.Services.CreateScope();
var user1Runner = user1Scope.ServiceProvider.GetRequiredService<Runner>();
var user2Runner = user2Scope.ServiceProvider.GetRequiredService<Runner>();
int bookId = await user1Runner.PrepareUpdateAsync("user1");
await user2Runner.PrepareUpdateAsync("user2", bookId);

// update
await user1Runner.UpdateAsync();
await user2Runner.UpdateAsync();

// check for the winner
using var checkScope = host.Services.CreateScope();
var runner = checkScope.ServiceProvider.GetRequiredService<Runner>();
string updatedTitle = await runner.GetUpdatedTitleAsyc(bookId);
Console.Write("this is the winner: ");
Console.WriteLine(updatedTitle);

await runner.DeleteDatabaseAsync();
