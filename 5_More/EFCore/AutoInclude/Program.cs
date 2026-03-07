
var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BooksConnection");
builder.Services.AddDbContext<BooksContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<Runner>();
using var host = builder.Build();

{
    await using var scope = host.Services.CreateAsyncScope();

    var runner = scope.ServiceProvider.GetRequiredService<Runner>();

    await runner.CreateTheDatabaseAsync();

    await runner.AutoIncludeLoading();

    await runner.DeleteDatabaseAsync();
}
