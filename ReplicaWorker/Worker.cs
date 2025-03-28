using TFMessage.worker.service;

namespace ReplicaWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IReplicaService _replicaService;

    public Worker(ILogger<Worker> logger,IReplicaService replicaService)
    {
        _logger = logger;
        _replicaService = replicaService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        if (_logger.IsEnabled(LogLevel.Information))
        {
          _logger.LogInformation($"Replicating files: {DateTimeOffset.Now}" );
        }

        _replicaService.Scan();
        await Task.Delay(30000, stoppingToken);
      }
    }
}

