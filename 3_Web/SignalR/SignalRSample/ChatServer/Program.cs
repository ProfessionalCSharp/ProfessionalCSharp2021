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

app.MapHub<ChatHub>("/chat");
app.MapHub<GroupChatHub>("/groupchat");

app.Map("/", async (HttpContext context) =>
{
    StringBuilder sb = new();
    sb.Append("<h1>SignalR Sample</h1>");
    sb.Append("<div>Open <a href='/ChatWindow.html'>ChatWindow</a> for communication</div>");
    await context.Response.WriteAsync(sb.ToString());
});

app.Run();