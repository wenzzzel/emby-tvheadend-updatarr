namespace emby_tvheadend_updatarr.Models;

public class TunerHost
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public required string Type { get; set; }
    public required string FriendlyName { get; set; }
    public required string SetupUrl { get; set; }
    public required bool ImportFavoritesOnly { get; set; }
    public required bool PreferEpgChannelImages { get; set; }
    public required bool PreferEpgChannelNumbers { get; set; }
    public required bool AllowHWTranscoding { get; set; }
    public required bool AllowMappingByNumber { get; set; }
    public required bool ImportGuideData { get; set; }
    public required int TunerCount { get; set; }
    public required int DataVersion { get; set; }
}