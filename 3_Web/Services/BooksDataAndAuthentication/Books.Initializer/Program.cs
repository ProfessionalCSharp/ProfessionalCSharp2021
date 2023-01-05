using Books.Data;
using Books.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        string booksConnection = context.Configuration.GetConnectionString("BooksConnection") ?? throw new InvalidOperationException("BooksConnection not configured");
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(booksConnection);
        });

        services.AddTransient<SampleChapters>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var booksContext = scope.ServiceProvider.GetRequiredService<BooksContext>();
await booksContext.Database.EnsureCreatedAsync();

var sampledata = scope.ServiceProvider.GetRequiredService<SampleChapters>();
var chapters = sampledata.GetSampleChapters();
await booksContext.Chapters.AddRangeAsync(chapters);
await booksContext.SaveChangesAsync();
