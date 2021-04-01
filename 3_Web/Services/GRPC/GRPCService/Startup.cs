using Books.Data;
using Books.Services;
using GRPCService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GRPCService
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => _configuration = configuration;

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddDbContext<IBookChapterService, BooksContext>(options =>
            {
                string connectionString = _configuration.GetConnectionString("BooksConnection");
                if (connectionString is null) throw new InvalidOperationException("Configure the connection string");
                options.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<BooksService>();
                endpoints.MapGrpcService<SensorService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Use a gRPC client!");
                });
            });
        }
    }
}
