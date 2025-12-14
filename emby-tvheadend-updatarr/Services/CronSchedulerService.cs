using emby_tvheadend_updatarr.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System.Threading;

namespace emby_tvheadend_updatarr.Services;

public class CronSchedulerService : BackgroundService
{
    private readonly ILogger<CronSchedulerService> _logger;
    private readonly AppConfiguration _config;
    private readonly CrontabSchedule? _schedule;

    public CronSchedulerService(
        ILogger<CronSchedulerService> logger,
        AppConfiguration config)
    {
        _logger = logger;
        _config = config;

        if (!string.IsNullOrEmpty(_config.CronExpression))
        {
            try
            {
                _schedule = CrontabSchedule.Parse(_config.CronExpression);
                _logger.LogInformation("Cron scheduler initialized with expression: {CronExpression}", _config.CronExpression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid cron expression: {CronExpression}", _config.CronExpression);
                throw new ArgumentException($"Invalid cron expression: {_config.CronExpression}", ex);
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_config.RunOnce)
        {
            //Run your stuff here
            return;
        }

        if (_schedule == null)
        {
            _logger.LogWarning("No cron expression provided and RunOnce is false. Service will exit.");
            return;
        }

        _logger.LogInformation("Cron scheduler started");

        while (!stoppingToken.IsCancellationRequested)
        {
            var nextOccurrence = _schedule.GetNextOccurrence(DateTime.UtcNow);
            var delay = nextOccurrence - DateTime.UtcNow;

            if (delay.TotalMilliseconds > 0)
            {
                _logger.LogInformation("Next execution scheduled for: {NextExecution} (in {DelayMinutes:F2} minutes)", 
                    nextOccurrence, delay.TotalMinutes);
                
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogInformation(ex, "Cron scheduler cancelled");
                    break;
                }
            }

            if (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Executing task...");
                try
                {
                    // Execute scheduled task here
                    _logger.LogInformation("Task completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing task");
                    // Continue the loop to try again at the next scheduled time
                }
            }
        }

        _logger.LogInformation("Cron scheduler stopped");
    }
}
