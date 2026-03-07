global using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BooksConnection");
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<BankContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddDbContext<MenusContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<BooksRunner>();
builder.Services.AddScoped<BankRunner>();
builder.Services.AddScoped<MenusRunner>();

var host = builder.Build();

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
