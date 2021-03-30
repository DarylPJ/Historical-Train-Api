using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceDetails
{
    public class Location
    {
        [JsonProperty("Location")]
        public string Station { get; set; }

        [JsonProperty("gbtt_ptd")]
        public string TimetabledDeparture { get; set; }

        [JsonProperty("gbtt_pta")]
        public string TimetabledArrival { get; set; }

        [JsonProperty("actual_td")]
        public string ActualDeparture { get; set; }

        [JsonProperty("actual_ta")]
        public string ActualArrival { get; set; }

        [JsonProperty("late_canc_reason")]
        public string Reason { get; set; }
    }
}
