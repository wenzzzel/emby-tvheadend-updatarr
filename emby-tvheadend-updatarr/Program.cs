using emby_tvheadend_updatarr.Models;
using emby_tvheadend_updatarr.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace emby_tvheadend_updatarr;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var appConfig = new AppConfiguration
                {
                    CronExpression = Environment.GetEnvironmentVariable("CRON_EXPRESSION"),
                    RunOnce = bool.Parse(Environment.GetEnvironmentVariable("RUN_ONCE") ?? "false")
                };

                services.AddSingleton(appConfig);
                
                services.AddHostedService<CronSchedulerService>();
            })
            .Build();

        await host.RunAsync();
    }
}