using Books.Models;

using BooksApi.Services;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v4", new OpenApiInfo { Title = "BooksApi", Version = "v4" });
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

app.MapGet("/bookchapters", async (IBookChapterService chapterService) =>
{
    var books = await chapterService.GetAllAsync();
    return TypedResults.Ok(books);
});

app.MapGet("/bookchapters/{id}", async Task<Results<NotFound, Ok<BookChapter>>> (IBookChapterService chapterService, Guid id) =>
{
    var chapter = await chapterService.FindAsync(id);
    if (chapter is null)
    {
        return TypedResults.NotFound();
    }
    else
    {
        return TypedResults.Ok(chapter);
    }
});

app.MapPost("/bookchapters", async Task<Results<BadRequest, Created<BookChapter>>> (IBookChapterService chapterService, BookChapter chapter) =>
{
    if (chapter is null)
    {
        return TypedResults.BadRequest();
    }
    await chapterService.AddAsync(chapter);
    return TypedResults.Created($"/bookchapters/{chapter.Id}", chapter);
});

app.MapPut("/bookchapters/{id}", async Task<Results<BadRequest, NotFound, NoContent>> (IBookChapterService chapterService, BookChapter chapter, Guid id) =>
{
    if (chapter is null || id != chapter.Id)
    {
        return TypedResults.BadRequest();
    }

    var existingChapter = await chapterService.FindAsync(id);
    if (existingChapter is null) 
    {
        return TypedResults.NotFound();
    }
    else
    {
        return TypedResults.NoContent();
    }
});

app.MapDelete("/bookchapters/{id}", async (IBookChapterService chapterService, Guid id) =>
{
    await chapterService.RemoveAsync(id);
    return TypedResults.Ok();
});


app.MapGet("/init", async (SampleChapters sampleChapters, HttpContext context) =>
{
    sampleChapters.CreateSampleChapters();
    await context.Response.WriteAsync("sample chapters initialized");
});

app.Run();
