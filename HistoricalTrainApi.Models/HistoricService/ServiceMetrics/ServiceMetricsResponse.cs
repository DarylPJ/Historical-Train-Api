using Newtonsoft.Json;
using System.Collections.Generic;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
{
    public class ServiceMetricsResponse
    {
        [JsonProperty("header")]
        public Header Header { get; set; }

        public IList<Service> Services { get; } = new List<Service>();
    }
}
