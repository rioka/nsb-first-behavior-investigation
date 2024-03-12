using Microsoft.Extensions.Logging;
using NServiceBus.Pipeline;

namespace NsbFirstBehaviorInvestigation.Sample.Internals;

internal class NoOpInBehavior : Behavior<IIncomingLogicalMessageContext>
{
  private readonly ILogger<NoOpInBehavior> _logger;

  public Guid InstanceId { get; set; }

  public NoOpInBehavior(ILogger<NoOpInBehavior> logger)
  {
    _logger = logger;
    InstanceId = Guid.NewGuid();
    _logger.LogInformation("{Behavior}: instance {Id})", GetType().Name, InstanceId);
  }

  public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
  {
    await next();
  }
}