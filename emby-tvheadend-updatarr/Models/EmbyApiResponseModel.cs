using System.Collections.Generic;

namespace emby_tvheadend_updatarr.Models;

public class EmbyApiResponseModel
{
    public required List<TunerHost> TunerHosts { get; set; }
}