using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string connectionString = context.Configuration.GetConnectionString("MenusConnection") ?? throw new InvalidOperationException("Could not read MenusConnection");
        services.AddDbContextFactory<MenusContext>(options => 
            options.UseSqlServer(connectionString));

        services.AddScoped<Runner>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();

await runner.AddTwoRecordsWithOneTxAsync();
await runner.AddTwoRecordsWithTwoTxAsync();
await runner.TwoSaveChangesWithOneTxAsync();
await runner.AmbientTransactionsAsync();

await runner.DeleteDatabaseAsync();
