using Microsoft.AspNetCore.Mvc;

namespace WorkerService.Playground.Controllers;

[ApiController]
[Route("[controller]")]
public class InspectController : ControllerBase
{
    
    private readonly ILogger<InspectController> _logger;
    private readonly SharedMemory _shared;

    public InspectController(ILogger<InspectController> logger, SharedMemory shared)
    {
        _logger = logger;
        _shared = shared;
    }

    [HttpGet(Name = "shared-count")]
    public int Count()
    {
        return _shared.Values.Count;
    }
}

public class SharedMemory
{
    public List<long> Values = new();
    public void Add(long v)
    {
        Values.Add(v);
    }
}