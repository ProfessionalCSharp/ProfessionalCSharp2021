using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
await context.Database.MigrateAsync();
