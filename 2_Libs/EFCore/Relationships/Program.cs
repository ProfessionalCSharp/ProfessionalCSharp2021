global using Microsoft.EntityFrameworkCore;

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
        services.AddDbContext<BankContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddDbContext<MenusContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<BooksRunner>();
        services.AddScoped<BankRunner>();
        services.AddScoped<MenusRunner>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var booksRunner = scope.ServiceProvider.GetRequiredService<BooksRunner>();
    await booksRunner.CreateTheDatabaseAsync();
    await booksRunner.GetBooksForAuthorAsync();

    var bankRunner = scope.ServiceProvider.GetRequiredService<BankRunner>();
    await bankRunner.CreateTheDatabaseAsync();
    await bankRunner.AddSampleDataAsync();
    await bankRunner.QuerySampleAsync();

    var menusRunner = scope.ServiceProvider.GetRequiredService<MenusRunner>();
    await menusRunner.CreateTheDatabaseAsync();
}
