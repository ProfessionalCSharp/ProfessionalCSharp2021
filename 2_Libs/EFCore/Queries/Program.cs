using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MenusConnection");
builder.Services.AddDbContext<MenusContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<Runner>();

var host = builder.Build();

using var scope = host.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<Runner>();
await runner.CreateDatabaseAsync();
await runner.FindByKeyAsync(23);
await runner.SingleOrDefaultAsync("menu 27");
await runner.FirstOrDefaultAsync("menu 27");
await runner.WhereAsync();
await runner.PagingAsync(10, 5);
await runner.GetAllMenusUsingAsyncStream();
// await runner.RawSqlAsync("menu 27");
runner.UseCompiledQuery();
await runner.UseCompiledQueryAsync();
await runner.UseEFunctions("24");

await runner.DeleteDatabaseAsync();
