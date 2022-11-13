using AsyncStreaming.Shared;

using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreAPIAsyncStreaming.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ADeviceController : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<DeviceData> GetSomeData()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new DeviceData($"text {Random.Shared.Next(200)}", DateTime.Now, i);
            await Task.Delay(100);
        }
    }
}
