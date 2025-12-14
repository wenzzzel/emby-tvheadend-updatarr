namespace emby_tvheadend_updatarr.Models;

public class AppConfiguration
{
    public string? CronExpression { get; set; }
    public bool RunOnce { get; set; } = false;
}
