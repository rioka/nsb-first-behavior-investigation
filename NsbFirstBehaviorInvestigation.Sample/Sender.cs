using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NsbFirstBehaviorInvestigation.Sample.Messages;

namespace NsbFirstBehaviorInvestigation.Sample;

internal class Sender : BackgroundService
{
  private readonly IMessageSession _messageSession;
  private readonly ILogger<Sender> _logger;

  public Sender(IMessageSession messageSession, ILogger<Sender> logger)
  {
    _messageSession = messageSession;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    try
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        await _messageSession
          .SendLocal(new SaySomething() {
            Text = $"Hi, there! It's {DateTime.UtcNow:HH:mm:ss} here"
          }, stoppingToken)
          .ConfigureAwait(false);

        _logger.LogInformation("Message sent!");

        await Task
          .Delay(5_000, stoppingToken)
          .ConfigureAwait(false);
      }
    }
    catch (OperationCanceledException)
    {
      // graceful shutdown
    }
  }
}