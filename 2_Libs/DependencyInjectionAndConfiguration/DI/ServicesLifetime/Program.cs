class Program
{
    static void Main(string mode)
    {
        switch (mode)
        {
            case "singletonandtransient":
                SingletonAndTransient();
                break;
            case "scoped":
                UsingScoped();
                break;
            case "custom":
                CustomFactories();
                break;
            default:
                Usage();
                break;
        }
    }

    static void Usage()
    {
        Console.WriteLine("Invoke the application with --mode [singletonandtransient|scoped|custom]");
        return;
    }

    private static void CustomFactories()
    {
        IServiceB CreateServiceBFactory(IServiceProvider provider) =>
            new ServiceB(provider.GetRequiredService<INumberService>(), 
                provider.GetRequiredService<IOptions<ConfigurationB>>());

        Console.WriteLine(nameof(CustomFactories));

        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                NumberService numberService = new();

                services.AddSingleton<INumberService>(numberService);  // add existing
                services.Configure<ConfigurationB>(config => config.Mode = "factory");
                services.AddTransient<IServiceB>(CreateServiceBFactory); // use a factory
                services.Configure<ConfigurationA>(config => config.Mode = "singleton");
                services.AddSingleton<IServiceA, ServiceA>();
            }).Build();

        IServiceA a1 = host.Services.GetRequiredService<IServiceA>();
        IServiceA a2 = host.Services.GetRequiredService<IServiceA>();
        IServiceB b1 = host.Services.GetRequiredService<IServiceB>();
        IServiceB b2 = host.Services.GetRequiredService<IServiceB>();
        Console.WriteLine();
    }

    private static void UsingScoped()
    {
        Console.WriteLine(nameof(UsingScoped));

        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {                
                services.AddSingleton<INumberService, NumberService>();
                services.Configure<ConfigurationA>(config => config.Mode = "scoped");
                services.AddScoped<IServiceA, ServiceA>();
                services.Configure<ConfigurationB>(config => config.Mode = "singleton");
                services.AddSingleton<IServiceB, ServiceB>();
                services.Configure<ConfigurationC>(config => config.Mode = "transient");
                services.AddTransient<IServiceC, ServiceC>();
            }).Build();

        // the using statement is used here to end scope1 early         
        using (IServiceScope scope1 = host.Services.CreateScope())
        {
            IServiceA a1 = scope1.ServiceProvider.GetRequiredService<IServiceA>();
            a1.A();
            IServiceA a2 = scope1.ServiceProvider.GetRequiredService<IServiceA>();
            a2.A();
            IServiceB b1 = scope1.ServiceProvider.GetRequiredService<IServiceB>();
            b1.B();
            IServiceC c1 = scope1.ServiceProvider.GetRequiredService<IServiceC>();
            c1.C();
            IServiceC c2 = scope1.ServiceProvider.GetRequiredService<IServiceC>();
            c2.C();
        }
        Console.WriteLine("end of scope1");

        using (IServiceScope scope2 = host.Services.CreateScope())
        {
            IServiceA a3 = scope2.ServiceProvider.GetRequiredService<IServiceA>();
            a3.A();
            IServiceB b2 = scope2.ServiceProvider.GetRequiredService<IServiceB>();
            b2.B();
            IServiceC c3 = scope2.ServiceProvider.GetRequiredService<IServiceC>();
            c3.C();
        }
        Console.WriteLine("end of scope2");
        Console.WriteLine();
    }

    private static void SingletonAndTransient()
    {
        Console.WriteLine(nameof(SingletonAndTransient));

        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.Configure<ConfigurationA>(config => config.Mode = "singleton");
                services.AddSingleton<IServiceA, ServiceA>();
                services.Configure<ConfigurationB>(config => config.Mode = "transient");
                services.AddTransient<IServiceB, ServiceB>();
                services.AddTransient<ControllerX>();
                services.AddSingleton<INumberService, NumberService>();
            }).Build();

        Console.WriteLine($"requesting {nameof(ControllerX)}");

        ControllerX x = host.Services.GetRequiredService<ControllerX>();
        x.M();
        x.M();

        Console.WriteLine($"requesting {nameof(ControllerX)}");

        ControllerX x2 = host.Services.GetRequiredService<ControllerX>();
        x2.M();

        Console.WriteLine();
    }
}
