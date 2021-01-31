using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace HttpServerSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<GenerateHtml>();
            services.AddSingleton<Formula1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GenerateHtml generateHtml, Formula1 formula1)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/api/racers", async context =>
                {
                    await context.Response.WriteAsJsonAsync(formula1.GetChampions());
                });
                endpoints.MapGet("/", async context =>
                {
                    byte[] content = generateHtml.GetHtmlContent(context.Request);
                    await context.Response.WriteAsync(Encoding.UTF8.GetString(content));
                });
            });
        }
    }
}
