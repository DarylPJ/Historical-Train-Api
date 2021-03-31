using Newtonsoft.Json;
using System.Collections.Generic;

namespace HistoricalTrainApi.Models.HistoricService.ServiceDetails
{
    public class ServiceAttributesDetails
    {
        [JsonProperty("date_of_service")]
        public string DateOfService { get; set; }

        [JsonProperty("toc_code")]
        public string TrainOperator { get; set; }

        [JsonProperty("rid")]
        public string Rid { get; set; }

        [JsonProperty("locations")]
        public IList<LocationDetail> Locations { get; } = new List<LocationDetail>();
    }
}
