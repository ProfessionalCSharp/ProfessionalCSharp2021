using BookModels;
using EventViews.Models;
using WebAppSample.Models;
using WebAppSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSqlServer<BooksContext>(
    builder.Configuration.GetConnectionString("BooksConnection"));
builder.Services.AddSingleton<MenuSamplesService>();
builder.Services.AddSingleton<IEventsService, Formula1Events>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/init", (BooksContext context) =>
{
    context.Database.EnsureCreated();

});

app.Run();
