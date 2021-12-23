public static class GreetingServiceExtensions
{
    public static IServiceCollection AddGreetingService(this IServiceCollection services, IConfiguration config)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (config == null) throw new ArgumentNullException(nameof(config));

        services.Configure<GreetingServiceOptions>(config);
        return services.AddTransient<IGreetingService, GreetingService>();
    }
}
