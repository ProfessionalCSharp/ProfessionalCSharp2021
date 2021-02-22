using CreateBooksDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string booksConnection = context.Configuration.GetConnectionString("BooksConnection");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlite(booksConnection);
        });
    }).Build();

Console.WriteLine("Creating the database...");
var booksContext = host.Services.GetRequiredService<BooksContext>();
bool created = booksContext.Database.EnsureCreated();
string resultMessage = created ? "Database successfully created." : "Database not created - does it exist already?";
Console.WriteLine(resultMessage);
