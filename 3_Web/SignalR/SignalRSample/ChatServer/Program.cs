using ChatServer.Hubs;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chat");
    endpoints.MapHub<GroupChatHub>("/groupchat");
    endpoints.Map("/", async context =>
    {
        StringBuilder sb = new();
        sb.Append("<h1>SignalR Sample</h1>");
        sb.Append("<div>Open <a href='/ChatWindow.html'>ChatWindow</a> for communication</div>");
        await context.Response.WriteAsync(sb.ToString());
    });
});
