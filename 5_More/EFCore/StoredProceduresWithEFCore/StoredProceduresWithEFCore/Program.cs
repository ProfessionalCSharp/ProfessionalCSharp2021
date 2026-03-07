using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StoredProceduresWithEFCore;
using StoredProceduresWithEFCore.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<BooksContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<Runner>();

using var host = builder.Build();

var runner = host.Services.GetRequiredService<Runner>();
await runner.RunAsync();
