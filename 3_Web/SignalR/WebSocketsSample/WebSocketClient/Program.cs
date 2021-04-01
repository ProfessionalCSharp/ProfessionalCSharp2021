using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Client - wait for server");
Console.ReadLine();
await RunAsync("wss://localhost:5001/samplesockets");
Console.WriteLine("Program end");
Console.ReadLine();

async Task RunAsync(string address)
{
    try
    {
        CancellationTokenSource cts = new();
        ClientWebSocket webSocket = new();
        webSocket.Options.AddSubProtocol("procsharp1.0");
        
        await webSocket.ConnectAsync(new Uri(address), CancellationToken.None);

        await SendAndReceiveAsync(webSocket, "A", cts.Token);
        await SendAndReceiveAsync(webSocket, "B", cts.Token);
        await webSocket.SendAsync(Encoding.UTF8.GetBytes("SERVERCLOSE").AsMemory(), WebSocketMessageType.Text, endOfMessage: true, cts.Token);
        byte[] buffer = new byte[4096];

        var result = await webSocket.ReceiveAsync(buffer.AsMemory(), cts.Token);
        Console.WriteLine($"received for close: {result.MessageType} {result.EndOfMessage} {Encoding.UTF8.GetString(buffer[..result.Count])}");
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", cts.Token);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

async Task SendAndReceiveAsync(WebSocket webSocket, string term, CancellationToken token)
{
    byte[] data = Encoding.UTF8.GetBytes($"REQUESTMESSAGES:{term}");
    var buffer = new byte[4096];

    await webSocket.SendAsync(data.AsMemory(), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
    ValueWebSocketReceiveResult result;
    bool sequenceEnd = false;
    do
    {
        result = await webSocket.ReceiveAsync(buffer.AsMemory(), token);
        string[] dataReceived = Encoding.UTF8.GetString(buffer[0..result.Count]).Split(Environment.NewLine);
        foreach (var line in dataReceived)
        {
            Console.WriteLine($"received {line}");
            if (line.StartsWith("EOS"))
            {
                sequenceEnd = true;
                Console.WriteLine("...ending sequence");
            }
        }

    } while (!sequenceEnd || result.MessageType == WebSocketMessageType.Close);
}
