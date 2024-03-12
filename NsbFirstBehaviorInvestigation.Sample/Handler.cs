using Microsoft.Extensions.Logging;
using NsbFirstBehaviorInvestigation.Sample.Messages;

namespace NsbFirstBehaviorInvestigation.Sample;

internal class Handler : IHandleMessages<SaySomething>
{
  private readonly ILogger<Handler> _logger;

  public Handler(ILogger<Handler> logger)
  {
    _logger = logger;
  }

  public Task Handle(SaySomething message, IMessageHandlerContext context)
  {
    _logger.LogInformation("I'll say '{Message}'", message.Text);
    return Task.CompletedTask;
  }
}