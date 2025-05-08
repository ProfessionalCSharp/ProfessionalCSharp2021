var builder = DistributedApplication.CreateBuilder(args);
var weatherapi = builder.AddProject<Projects.WeatherAPI>("weatherapi")
    .WithReplicas(3)
    .WithEnvironment("MyEnv1", "some value");

// var weatherApi = builder.AddProject("weatherapi", "../WeatherAPI/WeatherAPI.csproj");
builder.Build().Run();
