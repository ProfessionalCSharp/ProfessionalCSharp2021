using Microsoft.AspNetCore;

await WebHost.Start(async context =>
{
    await context.Response.WriteAsync("<h1>A Simple Host!</h1>");
}).WaitForShutdownAsync();
