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
                // Register DI like this: services.AddSingleton<IInterface, Implementation>();
                services.AddHostedService<CronSchedulerService>();
            })
            .Build();

        await host.RunAsync();
    }
}