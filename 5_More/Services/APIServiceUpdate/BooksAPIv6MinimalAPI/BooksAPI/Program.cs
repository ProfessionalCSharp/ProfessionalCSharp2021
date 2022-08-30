global using BooksAPI.Models;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSqlServer<BooksContext>(builder.Configuration.GetConnectionString("BooksConnection"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.MapGet("/api/books", async (BooksContext context) =>
{
    return await context.Books.ToListAsync();
});

app.MapGet("/api/books/{id}", async (int id, BooksContext context) =>
{
    var book = await context.Books.FindAsync(id);

    if (book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});

app.MapPost("/api/books", async (BooksContext context, Book book) =>
{
    context.Books.Add(book);
    await context.SaveChangesAsync();

    return Results.Created($"/api/books/{book.BookId}", book);
});

app.MapPut("/api/books/{id}", async (BooksContext context, Book book, int id) =>
{
    static bool BookExists(BooksContext context, int id)
    {
        return context.Books.Any(e => e.BookId == id);
    }

    if (id != book.BookId)
    {
        return Results.BadRequest();
    }

    context.Entry(book).State = EntityState.Modified;

    try
    {
        await context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!BookExists(context, id))
        {
            return Results.NotFound();
        }
        else
        {
            throw;
        }
    }

    return Results.NoContent();
});

app.MapDelete("/api/books", async (BooksContext context, int id) =>
{
    var book = await context.Books.FindAsync(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    context.Books.Remove(book);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
