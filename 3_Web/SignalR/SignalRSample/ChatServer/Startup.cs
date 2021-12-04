using ChatServer.Hubs;

using System.Text;

namespace ChatServer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
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

    }
}
