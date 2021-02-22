using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CommandSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    string booksConnection = context.Configuration.GetConnectionString("BooksConnection");
                    services.Configure<RunnerConfiguration>(options =>
                    {
                        options.ConnectionString = booksConnection;
                    });
                    services.AddTransient<Runner>();
                })
                .Build();

            var runner = host.Services.GetRequiredService<Runner>();
            runner.ExecuteNonQuery();
            runner.ExecuteScalar();
            runner.ExecuteReader("Professional C#");
   
            Console.ReadLine();
        }
    }
}
