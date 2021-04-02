using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRStreaming.Hubs
{
    public record SensorData(int Val1, int Val2, DateTime TimeStamp);

    public class StreamingHub : Hub
    {
        public async IAsyncEnumerable<SensorData> GetSensorData([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            Random r = new();
            for (int i = 0; i < 1000; i++)
            {
                yield return new SensorData(r.Next(20), r.Next(20), DateTime.Now);
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
