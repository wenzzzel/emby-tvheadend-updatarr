using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using emby_tvheadend_updatarr.Models;
using Microsoft.Extensions.Logging;

namespace emby_tvheadend_updatarr.Services;

public interface IEmbyTunerService
{
    Task RefreshTvHeadendTunersAsync();
}

public class EmbyTunerService(HttpClient httpClient, AppConfiguration appConfig, ILogger<EmbyTunerService> logger) : IEmbyTunerService
{
    private const string _tunersEndpoint = "/emby/LiveTv/TunerHosts";

    public async Task RefreshTvHeadendTunersAsync()
    {
        var uriWithApiKey = _tunersEndpoint + "?api_key=" + appConfig.EmbyApiKey;

        var currentTunersResponse = await httpClient.GetAsync(uriWithApiKey);
        currentTunersResponse.EnsureSuccessStatusCode();

        var respString = await currentTunersResponse.Content.ReadAsStringAsync();
        var tuners = System.Text.Json.JsonSerializer.Deserialize<List<TunerHost>>(respString) ?? new List<TunerHost>();
        if (tuners.Count > 1)
        {
            logger.LogError("More than one tuner is not supported. Found {TunerCount} tuners.", tuners.Count);
            return;
        }

        if (tuners.Count < 1)
        {
            logger.LogWarning("No tuners found to refresh.");
            return;
        }

        logger.LogInformation("Found exactly one tuner. Refreshing by deleting and re-adding it.");

        var deleteResponse = await httpClient.DeleteAsync(uriWithApiKey + "&Id=" + tuners[0].Id);
        deleteResponse.EnsureSuccessStatusCode();
        logger.LogInformation("Successfully deleted the tuner. Now let's re-add it.");

        // Remove surrounding square brackets if present
        var body = respString;
        if (body.StartsWith('['))
            body = body.Substring(1);
        if (body.EndsWith(']'))
            body = body.Substring(0, body.Length - 1);

        var requestBody = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
        var refreshResult = await httpClient.PostAsync(uriWithApiKey, requestBody);
        refreshResult.EnsureSuccessStatusCode();
        logger.LogInformation("Successfully Re-added the tuner.");


        logger.LogInformation("Tuner refresh process completed.");
    }
}