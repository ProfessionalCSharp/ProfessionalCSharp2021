using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

using System.Globalization;

namespace ASPNETCoreLocalization;

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
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        services.AddRazorPages()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder)
            .AddDataAnnotationsLocalization();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        CultureInfo[] supportedCultures = { new("en-US"), new("en"), new("de-AT"), new("de") };

        RequestLocalizationOptions localizationOptions = new()
        {
            DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US")),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };

        app.UseRequestLocalization(localizationOptions);

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
