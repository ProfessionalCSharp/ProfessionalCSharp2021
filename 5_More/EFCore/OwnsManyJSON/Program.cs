
using OwnsMany;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OrdersConnection");
builder.Services.AddDbContext<OrdersContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<Runner>();
using var host = builder.Build();

{
    await using var scope = host.Services.CreateAsyncScope();

    var runner = scope.ServiceProvider.GetRequiredService<Runner>();

    await runner.CreateTheDatabaseAsync();
    await runner.CreateOrdersAsync();

    await runner.QueryOrdersAsync();

    await runner.DeleteDatabaseAsync();
}
