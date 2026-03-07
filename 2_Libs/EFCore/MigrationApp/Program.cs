var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BooksConnection");
builder.Services.AddDbContext<BooksContext>();

var host = builder.Build();

await using var scope = host.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
await context.Database.MigrateAsync();
