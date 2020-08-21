using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DIWithConfiguration
{
    class Program
    {
        static void Main()
        {
            Configuration = GetConfiguration();
            var container = RegisterServices();
            var controller = container.GetRequiredService<HomeController>();
            string result = controller.Hello("Katharina");
            Console.WriteLine(result);
        }

        static IConfiguration GetConfiguration()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");
            return  configBuilder.Build();
        }

        public static IConfiguration? Configuration { get; set; }

        static ServiceProvider RegisterServices()
        {
            if (Configuration == null)
            {
                throw new InvalidOperationException("setup configuration first");
            }
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddGreetingService(Configuration.GetSection("GreetingService"));
            services.AddTransient<HomeController>();
            return services.BuildServiceProvider();
        }
    }
}
