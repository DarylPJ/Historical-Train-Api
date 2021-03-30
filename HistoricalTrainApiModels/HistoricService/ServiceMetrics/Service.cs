using Newtonsoft.Json;
using System.Collections.Generic;

namespace HistoricalTrainApiModels.HistoricService.ServiceMetrics
{
    public class Service
    {
        [JsonProperty("serviceAttributesMetrics")]
        public ServiceAttributesMetrics ServiceAttributesMetrics { get; set; }

        public IList<Metric> Metrics { get; } = new List<Metric>();
    }
}