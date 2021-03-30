using Newtonsoft.Json;
using System.Collections.Generic;

namespace HistoricalTrainApi.Models.HistoricService.ServiceMetrics
{
    public class ServiceAttributesMetrics
    {
        [JsonProperty("origin_location")]
        public string OriginLocation { get; set; }

        [JsonProperty("destination_location")]
        public string DestinationLocation { get; set; }

        [JsonProperty("gbtt_ptd")]
        public string TimetabledDeparture { get; set; }

        [JsonProperty("gbtt_pta")]
        public string TimetabledArrival { get; set; }

        [JsonProperty("toc_code")]
        public string TrainOperator { get; set; }

        [JsonProperty("matched_services")]
        public int MatchedServices { get; set; }

        [JsonProperty("rids")]
        public IList<string> Rids { get; } = new List<string>();
    }
}
