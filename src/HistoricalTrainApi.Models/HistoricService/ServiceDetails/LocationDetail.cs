using Newtonsoft.Json;

namespace HistoricalTrainApi.Models.HistoricService.ServiceDetails
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
            ActualArrival = FormatTime(ActualArrival),
            ActualDeparture = FormatTime(ActualDeparture),
            DelayReason = DelayReason,
            TimetabledDeparture = FormatTime(TimetabledDeparture),
            TimetabledArrival = FormatTime(TimetabledArrival)
        };

        private string FormatTime(string time)
        {
            if (!string.IsNullOrWhiteSpace(time))
            {
                return $"{time.Substring(0, 2)}:{time.Substring(2, 2)}";
            }

            return time;
        }
    }
}
