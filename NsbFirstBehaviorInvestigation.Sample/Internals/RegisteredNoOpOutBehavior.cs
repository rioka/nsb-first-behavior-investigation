using Microsoft.Extensions.Logging;
using NServiceBus.Pipeline;

namespace NsbFirstBehaviorInvestigation.Sample.Internals;

/// <summary>
/// An outgoing-pipeline behavior that is registered as singleton in <see cref="IServiceProvider"/>.
/// </summary>
internal class RegisteredNoOpOutBehavior : Behavior<IOutgoingLogicalMessageContext>
{
  private readonly ILogger<RegisteredNoOpOutBehavior> _logger;

  public Guid InstanceId { get; set; }

  public RegisteredNoOpOutBehavior(ILogger<RegisteredNoOpOutBehavior> logger)
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