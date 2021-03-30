using System.Collections.Generic;

namespace HistoricalTrainApi.Models
{
    public class HistoricalRecord
    {
        public string OriginLocation { get; set; }

        public string DestinationLocation { get; set; }

        public IDictionary<string, HistoricalData> LocationData { get; } = 
            new Dictionary<string, HistoricalData>();
    }
}
