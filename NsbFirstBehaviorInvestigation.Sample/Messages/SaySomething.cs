namespace NsbFirstBehaviorInvestigation.Sample.Messages;

internal class SaySomething : ICommand
{
  public string Text { get; set; }  
}