var builder = DistributedApplication.CreateBuilder(args);
var dataStore = builder.Configuration["DataStore"] ?? "SqlServer";



var apiService = builder.AddProject<Projects.MultipleProvidersWithAspire_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health")
    .WithEnvironment("DataStore", dataStore);

if (dataStore == "SqlServer")
{
    var db = builder.AddSqlServer("sqlserver")
        .AddDatabase("books2025");

    apiService
        .WithReference(db)
        .WaitFor(db);
}
else
{
    var db = builder.AddOracle("oracle");
//       .AddDatabase("books2025"); use with Aspire 9.2

    apiService
        .WithReference(db)
        .WaitFor(db);
}

builder.AddProject<Projects.MultipleProvidersWithAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
