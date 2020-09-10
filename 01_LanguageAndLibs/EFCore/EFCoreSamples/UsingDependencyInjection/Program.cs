using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UsingDependencyInjection;

using var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        var booksConnection = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(booksConnection);
        });
        services.AddTransient<BooksService>();
    }).Build();

var service = host.Services.GetRequiredService<BooksService>();
await service.CreateDataaseAsync();
await service.AddBooksAsync();
await service.ReadBooksAsync();
await service.DeleteDatabaseAsync();
