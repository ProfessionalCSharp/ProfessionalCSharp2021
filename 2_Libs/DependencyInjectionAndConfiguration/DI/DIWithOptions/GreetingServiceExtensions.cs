namespace DISample;

public static class GreetingServiceExtensions
{
    public static IServiceCollection AddGreetingService(this IServiceCollection collection,
        Action<GreetingServiceOptions> setupAction)
    {
        ArgumentNullException.ThrowIfNull(collection);
        ArgumentNullException.ThrowIfNull(setupAction);

        collection.Configure(setupAction);
        return collection.AddTransient<IGreetingService, GreetingService>();
    }
}
