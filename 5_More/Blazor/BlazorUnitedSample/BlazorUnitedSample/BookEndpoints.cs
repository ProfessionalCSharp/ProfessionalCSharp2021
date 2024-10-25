using BlazorUnitedSample.Data;
using BlazorUnitedSample.Models;
using BlazorUnitedSample.Services;

using Microsoft.AspNetCore.Http.HttpResults;
namespace BlazorUnitedSample;

public static class BookEndpoints
{
    public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/init", async (BooksContext db) =>
        {
            if (await db.Database.EnsureCreatedAsync())
            {

            }

            await db.Book.AddRangeAsync(
            [
                    new Book { Title = "The Great Gatsby", Publisher = "Scribner" },
                    new Book { Title = "To Kill a Mockingbird", Publisher = "J.B. Lippincott & Co." },
                    new Book { Title = "1984", Publisher = "Secker & Warburg" },
                    new Book { Title = "The Catcher in the Rye", Publisher = "Little, Brown and Company" }
                ]);
            await db.SaveChangesAsync();
            return Results.Ok();
        });

        group.MapGet("/", async (IBooksService service) =>
        {
            return await service.GetBooksAsync();
        })
        .WithName("GetAllBooks")
        .WithOpenApi();

       

        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int id, IBooksService service) =>
        {
            Book? book = await service.GetBookByIdAsync(id);
            if (book is null)
            {
                return TypedResults.NotFound();
            }
            else
            {
                return TypedResults.Ok(book);
            }
        })
        .WithName("GetBookById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Book book, IBooksService service) =>
        {
            var affected = await service.UpdateBookAsync(id, book);
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBook")
        .WithOpenApi();

        group.MapPost("/", async (Book book, IBooksService service) =>
        {
            book = await service.AddBookAsync(book);
            return TypedResults.Created($"/api/Book/{book.Id}",book);
        })
        .WithName("CreateBook")
        .WithOpenApi();

        //group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, BooksContext db) =>
        //{
        //    var affected = await db.Book
        //        .Where(model => model.Id == id)
        //        .ExecuteDeleteAsync();
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("DeleteBook")
        //.WithOpenApi();
    }
}
