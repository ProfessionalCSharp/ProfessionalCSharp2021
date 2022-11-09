using System.Text.Encodings.Web;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Map("/echo", async context =>
{
    string? data = context.Request.Query["x"];
    data ??= "no x received";
    await context.Response.WriteAsync(data);
});

app.Map("/echoenc", async context =>
{
    string? data = context.Request.Query["x"];
    data ??= "no x received";
    await context.Response.WriteAsync(HtmlEncoder.Default.Encode(data));
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
