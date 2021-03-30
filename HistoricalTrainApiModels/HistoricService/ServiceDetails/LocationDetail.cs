using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceDetails
{
    public class LocationDetail
    {
        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("gbtt_ptd")]
        public string TimetabledDeparture { get; set; }

        [JsonProperty("gbtt_pta")]
        public string TimetabledArrival { get; set; }

        [JsonProperty("actual_td")]
        public string ActualDeparture { get; set; }

        [JsonProperty("actual_ta")]
        public string ActualArrival { get; set; }

        [JsonProperty("late_canc_reason")]
        public string DelayReason { get; set; }

        public HistoricalData ToHistoricalData() => new HistoricalData
        {
            ActualArrival = ActualArrival,
            ActualDeparture = ActualDeparture,
            DelayReason = DelayReason,
            TimetabledDeparture = TimetabledDeparture,
            TimetabledArrival = TimetabledArrival
        };
    }
}
