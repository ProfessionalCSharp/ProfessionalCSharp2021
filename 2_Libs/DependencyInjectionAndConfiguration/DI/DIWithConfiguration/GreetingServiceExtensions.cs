namespace DISample;

public static class GreetingServiceExtensions
{
    public static IServiceCollection AddGreetingService(this IServiceCollection services, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(config);

        services.Configure<GreetingServiceOptions>(config);
        return services.AddTransient<IGreetingService, GreetingService>();
    }
}
