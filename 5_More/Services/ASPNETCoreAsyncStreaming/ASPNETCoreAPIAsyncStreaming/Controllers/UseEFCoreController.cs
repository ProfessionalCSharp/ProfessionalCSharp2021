using ASPNETCoreAPIAsyncStreaming.Models;

using AsyncStreaming.Shared;

using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreAPIAsyncStreaming.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UseEFCoreController : ControllerBase
{
    private readonly SomeDataContext _dataContext;

    public UseEFCoreController(SomeDataContext dataContext)
    {
        ArgumentNullException.ThrowIfNull(dataContext);
        _dataContext = dataContext;
    }

    [HttpGet]
    public IAsyncEnumerable<SomeData> GetSomeData()
    {
        return _dataContext.SomeData.AsAsyncEnumerable();
    }
}

