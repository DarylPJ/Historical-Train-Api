using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceMetrics
{
    public class Metric
    {
        [JsonProperty("tolerance_value")]
        public int ToleranceValue { get; set; }

        [JsonProperty("num_not_tolerance")]
        public int NumberOutsideTolerance { get; set; }

        [JsonProperty("num_tolerance")]
        public int NumberInsideTolerance { get; set; }

        [JsonProperty("percent_tolerance")]
        public int PercentageInsideTolerance { get; set; }

        [JsonProperty("global_tolerance")]
        public bool GlobalTolerance { get; set; }
    }
}
