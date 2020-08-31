using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>()
                .UseKestrel()
                .UseUrls("https://localhost:5020", "http://localhost:5021");
        });

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                byte[] content = GetHtmlContent(context.Request);
                await context.Response.WriteAsync(Encoding.UTF8.GetString(content));
            });
        });
    }

    private static string s_htmlFormat =
        "<!DOCTYPE html><html><head><title>{0}</title></head>" +
        "<body>{1}</body></html>";

    private static byte[] GetHtmlContent(HttpRequest request)
    {
        string title = "Sample Listener using Kestrel";

        string content = $"<h1>Hello from the server</h1>" +
            $"<h2>Header Info</h2>" +
            $"{string.Join(' ', GetHeaderInfo(request.Headers))}" +
            $"<h2>Request Object Information</h2>" +
            $"{string.Join(' ', GetRequestInfo(request))}";

        string html = string.Format(s_htmlFormat, title, content);
        return Encoding.UTF8.GetBytes(html);
    }

    private static IEnumerable<string> GetRequestInfo(HttpRequest request)
    {
        var properties = request.GetType().GetProperties();
        var values = new List<(string Key, string Value)>();
        foreach (var property in properties)
        {
            try
            {
                string value = property.GetValue(request)?.ToString() ?? string.Empty;
                values.Add((property.Name, value));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{property.Name}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"\t{ex.InnerException.Message}");
                }
            }
        }
        return values.Select(v => $"<div>{v.Key}: {v.Value}</div>");
    }

    private static IEnumerable<string> GetHeaderInfo(IHeaderDictionary headers)
    {
        var values = new List<(string Key, string Value)>();
        var keys = headers.Keys;
        foreach (var key in keys)
        {
            if (headers.TryGetValue(key, out var value))
            {
                values.Add((key, value));
            }
        }
        return values.Select(v => $"<div>{v.Key}: {v.Value}</div>");
    }
}
