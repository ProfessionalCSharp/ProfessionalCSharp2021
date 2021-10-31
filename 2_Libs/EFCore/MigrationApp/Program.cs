using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>();
    })
    .Build();

await using var scope = host.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
await context.Database.MigrateAsync();
