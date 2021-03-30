namespace HistoricalTrainApiModels
{
    public class HistoricalData
    {
        public string TimetabledDeparture { get; set; }

        public string TimetabledArrival { get; set; }

        public string ActualDeparture { get; set; }

        public string ActualArrival { get; set; }

        public string DelayReason { get; set; }
    }
}
