using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServicesLifetime
{
    class Program 
    {
        static void Main()
        {
            SingletonAndTransient();
            //UsingScoped();
            //CustomFactories();
        }

        private static void CustomFactories()
        {
            Console.WriteLine(nameof(CustomFactories));

            IServiceB CreateServiceBFactory(IServiceProvider provider) =>
                new ServiceB(provider.GetRequiredService<INumberService>());

            ServiceProvider RegisterServices()
            {
                var numberService = new NumberService();

                var services = new ServiceCollection();
                services.AddSingleton<INumberService>(numberService);  // add existing

                services.AddTransient<IServiceB>(CreateServiceBFactory);  // use a factory
                services.AddSingleton<IServiceA, ServiceA>();
                return services.BuildServiceProvider();
            }

            using ServiceProvider container = RegisterServices();
            IServiceA a1 = container.GetRequiredService<IServiceA>();
            IServiceA a2 = container.GetRequiredService<IServiceA>();
            IServiceB b1 = container.GetRequiredService<IServiceB>();
            IServiceB b2 = container.GetRequiredService<IServiceB>();
            Console.WriteLine();
        }

        private static void UsingScoped()
        {
            Console.WriteLine(nameof(UsingScoped));

            ServiceProvider RegisterServices()
            {
                var services = new ServiceCollection();
                services.AddSingleton<INumberService, NumberService>();
                services.AddScoped<IServiceA, ServiceA>();
                services.AddSingleton<IServiceB, ServiceB>();
                services.AddTransient<IServiceC, ServiceC>();
                return services.BuildServiceProvider();
            }

            using ServiceProvider container = RegisterServices();
            using (IServiceScope scope1 = container.CreateScope())
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

            using (IServiceScope scope2 = container.CreateScope())
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

            ServiceProvider RegisterServices()
            {
                IServiceCollection services = new ServiceCollection();
                services.AddSingleton<IServiceA, ServiceA>();
                services.AddTransient<IServiceB, ServiceB>();
                // services.AddSingleton<ControllerX>();
                services.Add(new ServiceDescriptor(typeof(ControllerX), typeof(ControllerX), ServiceLifetime.Transient));
                services.AddSingleton<INumberService, NumberService>();
                return services.BuildServiceProvider();
            }

            using ServiceProvider container = RegisterServices();
            Console.WriteLine($"requesting {nameof(ControllerX)}");

            ControllerX x = container.GetRequiredService<ControllerX>();
            x.M();
            x.M();

            Console.WriteLine($"requesting {nameof(ControllerX)}");

            ControllerX x2 = container.GetRequiredService<ControllerX>();
            x2.M();

            Console.WriteLine();
        }
    }
}
