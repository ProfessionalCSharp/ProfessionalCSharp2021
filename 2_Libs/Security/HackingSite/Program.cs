var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseStaticFiles();

app.Map("/", async context =>
{
    await context.Response.WriteAsync("Invoke the static pages from this site while the site to be hacked is active");

});

app.Run();
