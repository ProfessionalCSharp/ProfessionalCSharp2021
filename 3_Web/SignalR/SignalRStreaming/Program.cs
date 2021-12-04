using SignalRStreaming.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<StreamingHub>("/stream");
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("SignalR Streaming Sample");
    });
});

app.Run();

