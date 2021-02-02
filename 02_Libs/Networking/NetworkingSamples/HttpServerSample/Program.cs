using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HttpServerSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureKestrel(kestrelOptions =>
                        {
                            kestrelOptions.AddServerHeader = true;
                            kestrelOptions.AllowResponseHeaderCompression = true;
                            kestrelOptions.Limits.Http2.MaxStreamsPerConnection = 10;
                            kestrelOptions.Limits.MaxConcurrentConnections = 20;
                            kestrelOptions.ConfigureHttpsDefaults(httpsConfig =>
                            {
                                
                            });
                        })
                        .UseUrls("http://localhost:5020", "https://localhost:5021");                       
                });
    }
}
