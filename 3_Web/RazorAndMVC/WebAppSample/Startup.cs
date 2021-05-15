using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookModels;
using EventViews.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppSample.Models;
using WebAppSample.Services;

namespace WebAppSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BooksContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("BooksConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddRazorPages();
            services.AddSingleton<MenuSamplesService>();
            services.AddSingleton<IEventsService, Formula1Events>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BooksContext booksContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                booksContext.Database.EnsureCreated();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
