using WorkerService.Playground.Controllers;

namespace WorkerService.Playground
{
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
                var now = DateTimeOffset.Now;
                _logger.LogInformation("Worker running at: {time}", now);

                _shared.Add(now.Ticks);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}