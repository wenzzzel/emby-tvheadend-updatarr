using System;
using System.Threading.Tasks;
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
                    RunOnce = bool.Parse(Environment.GetEnvironmentVariable("RUN_ONCE") ?? "false"),
                    EmbyApiKey = Environment.GetEnvironmentVariable("EMBY_API_KEY") ?? throw new ArgumentException("EMBY_API_KEY environment variable is not set"),
                    EmbyServerBaseUrl = Environment.GetEnvironmentVariable("EMBY_SERVER_BASE_URL") ?? throw new ArgumentException("EMBY_SERVER_BASE_URL environment variable is not set"),
                };

                services.AddSingleton(appConfig);
                services.AddHttpClient<IEmbyTunerService, EmbyTunerService>(client =>
                {
                    client.BaseAddress = new Uri(appConfig.EmbyServerBaseUrl);
                });

                services.AddHostedService<CronSchedulerService>();
            })
            .Build();

        await host.RunAsync();
    }
}