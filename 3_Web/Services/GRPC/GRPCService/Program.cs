using Books.Data;
using Books.Services;

using GRPCService.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<IBookChapterService, BooksContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("BooksConnection");
    if (connectionString is null) throw new InvalidOperationException("Configure the connection string");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BooksService>();
app.MapGrpcService<SensorService>();
app.MapGet("/", () => "Use a gRPC client!");

app.Run();
