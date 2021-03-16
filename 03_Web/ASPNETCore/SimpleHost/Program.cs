using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

WebHost.Start(async context =>
{
    await context.Response.WriteAsync("<h1>A Simple Host!</h1>");
}).WaitForShutdown();
