using Microsoft.Extensions.Logging;
using NServiceBus.Pipeline;

namespace NsbFirstBehaviorInvestigation.Sample.Internals;

internal class NoOpOutBehavior : Behavior<IOutgoingLogicalMessageContext>
{
  private readonly ILogger<NoOpOutBehavior> _logger;

  public Guid InstanceId { get; set; }

  public NoOpOutBehavior(ILogger<NoOpOutBehavior> logger)
  {
    _logger = logger;
    InstanceId = Guid.NewGuid();
    _logger.LogInformation("{Behavior}: instance {Id})", GetType().Name, InstanceId);
  }

  public override async Task Invoke(IOutgoingLogicalMessageContext context, Func<Task> next)
  {
    await next();
  }
}