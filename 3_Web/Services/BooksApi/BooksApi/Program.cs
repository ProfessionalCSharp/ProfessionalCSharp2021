using BooksApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v4", new OpenApiInfo { Title = "BooksApi", Version = "v3" });
});

builder.Services.AddSingleton<IBookChapterService, BookChapterService>();
builder.Services.AddScoped<SampleChapters>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v4/swagger.json", "Books API");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();


app.MapGet("/init", async (SampleChapters sampleChapters, HttpContext context) =>
{
    sampleChapters.CreateSampleChapters();
    await context.Response.WriteAsync("sample chapters initialized");
});
app.Run();

