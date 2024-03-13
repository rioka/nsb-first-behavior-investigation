using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NsbFirstBehaviorInvestigation.Sample.Internals;

namespace NsbFirstBehaviorInvestigation.Sample;

internal class Program
{
  public static async Task Main(string[] args)
  {
    var host = CreateHostBuilder(args)
      .Build();

    await host.RunAsync();
  }

  private static IHostBuilder CreateHostBuilder(string[] args)
  {
    var hb = Host
      .CreateDefaultBuilder(args);

    hb.ConfigureLogging((ctx, logging) => {
      
        logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        logging.AddConsole();
      })
      .UseConsoleLifetime();

    hb.UseNServiceBus(ctx => {

      var config = new EndpointConfiguration(nameof(NsbFirstBehaviorInvestigation));

      config.UseTransport(new LearningTransport());
      config.Pipeline.Register(typeof(NoOpOutBehavior), "No-op outgoing message behavior");
      config.Pipeline.Register(typeof(OtherNoOutOpBehavior), "Other no-op outgoing message behavior");
      config.Pipeline.Register(typeof(RegisteredNoOpOutBehavior), "Registered no-op outgoing message behavior");
      config.Pipeline.Register(typeof(NoOpInBehavior), "No-op incoming message behavior");

      return config;
    });

    hb.ConfigureServices(services => {

      services.AddSingleton<RegisteredNoOpOutBehavior>();
      services.AddHostedService<Sender>();
    });

    return hb;
  }
}