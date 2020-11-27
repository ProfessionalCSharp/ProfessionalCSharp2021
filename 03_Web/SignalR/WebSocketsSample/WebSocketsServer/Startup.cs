using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketsServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // configure keep alive interval, receive buffer size
            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/samplesockets", async context =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await SendMessagesAsync(context, webSocket, loggerFactory.CreateLogger("SendMessages"));
                    }
                    else
                    {
                        await context.Response.WriteAsync("<h1>Not a WebSocket request</h1>");
                    }

                });
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("<h1>Web Sockets Sample</h1>");
                });
            });

        }

        private async Task SendMessagesAsync(HttpContext context, WebSocket webSocket, ILogger logger)
        {
            var buffer = new byte[4096];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string content = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (content.StartsWith("REQUESTMESSAGES:"))
                    {
                        string message = content.Substring("REQUESTMESSAGES:".Length);
                        for (int i = 0; i < 10; i++)
                        {
                            string messageToSend = $"MESSAGE:{message} - {i}";
                            if (i == 9)
                            {
                                messageToSend += "\r\nEOS"; // send end of sequence to not let the client wait for another message
                            }
                            byte[] sendBuffer = Encoding.UTF8.GetBytes(messageToSend);
                            await webSocket.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                            logger.LogDebug("sent message {0}", messageToSend);
                            await Task.Delay(1000);
                        }
                    }

                    if (content.Equals("SERVERCLOSE"))
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye for now", CancellationToken.None);
                        logger.LogDebug("client sent close request, socket closing");
                        return;
                    }
                    else if (content.Equals("SERVERABORT"))
                    {
                        context.Abort();
                    }
                }

                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            }
        }
    }
}
