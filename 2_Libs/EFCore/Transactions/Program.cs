var builder = Host.CreateApplicationBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("MenusConnection") ?? throw new InvalidOperationException("Could not read MenusConnection");
builder.Services.AddDbContextFactory<MenusContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<Runner>();

var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddTwoRecordsWithOneTxAsync();
await runner.AddTwoRecordsWithTwoTxAsync();
await runner.TwoSaveChangesWithOneTxAsync();
await runner.AmbientTransactionsAsync();

await runner.DeleteDatabaseAsync();
