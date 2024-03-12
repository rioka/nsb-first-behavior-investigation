using Microsoft.Extensions.Logging;
using NServiceBus.Pipeline;

namespace NsbFirstBehaviorInvestigation.Sample.Internals;

internal class OtherNoOutOpBehavior : Behavior<IOutgoingPhysicalMessageContext>
{
  private readonly ILogger<OtherNoOutOpBehavior> _logger;

  public Guid InstanceId { get; set; }

  public OtherNoOutOpBehavior(ILogger<OtherNoOutOpBehavior> logger)
  {
    _logger = logger;
    InstanceId = Guid.NewGuid();
    _logger.LogInformation("{Behavior}: instance {Id})", GetType().Name, InstanceId);
  }

  public override async Task Invoke(IOutgoingPhysicalMessageContext context, Func<Task> next)
  {
    await next();
  }
}