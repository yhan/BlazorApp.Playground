namespace WebAPI.Playground;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly SharedMemory _shared;

    public Worker(ILogger<Worker> logger, SharedMemory shared)
    {
        _logger = logger;
        _shared = shared;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            _shared.Add(DateTime.Now.Ticks);

            await Task.Delay(1000, stoppingToken);
        }
    }
}

