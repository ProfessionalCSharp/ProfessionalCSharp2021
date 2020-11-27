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
    public class Messages
    {
        public const string REQUEST = "REQUESTMESSAGES:";
        public const string MESSAGE = "MESSAGE:";
        public const string EOS = "EOS";
        public const string SERVERCLOSE = "SERVERCLOSE";
        public const string SERVERABORT = "SERVERABORT";
    }

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
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync("procsharp1.0");
                        
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
            ValueWebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer.AsMemory(), CancellationToken.None);
            
            while (result.MessageType != WebSocketMessageType.Close)
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string content = Encoding.UTF8.GetString(buffer[..result.Count]);
                    logger.LogInformation($"server received {content}");
                    if (content.StartsWith(Messages.REQUEST))
                    {
                        string message = content[Messages.REQUEST.Length..];
                        for (int i = 0; i < 10; i++)
                        {
                            string messageToSend = $"{Messages.MESSAGE}{message} - {i}";
                            if (i == 9)
                            {
                                messageToSend += $"\r\n{Messages.EOS}"; // send end of sequence to not let the client wait for another message
                            }
                            byte[] sendBuffer = Encoding.UTF8.GetBytes(messageToSend);
                            await webSocket.SendAsync(sendBuffer.AsMemory(), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                            logger.LogDebug("sent message {messageToSend}", messageToSend);
                            await Task.Delay(1000);
                        }
                    }

                    if (content.Equals(Messages.SERVERCLOSE))
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye for now", CancellationToken.None);
                        logger.LogDebug("client sent close request, socket closing");
                        return;
                    }
                    else if (content.Equals(Messages.SERVERABORT))
                    {
                        context.Abort();
                    }
                }

                result = await webSocket.ReceiveAsync(buffer.AsMemory(), CancellationToken.None);
            }
        }
    }
}
