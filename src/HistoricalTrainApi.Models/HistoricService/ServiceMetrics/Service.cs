using Newtonsoft.Json;
using System.Collections.Generic;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
{
    public class Service
    {
        [JsonProperty("serviceAttributesMetrics")]
        public ServiceAttributesMetrics ServiceAttributesMetrics { get; set; }

        public IList<Metric> Metrics { get; } = new List<Metric>();
    }
}