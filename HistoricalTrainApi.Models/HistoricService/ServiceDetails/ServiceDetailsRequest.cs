using Newtonsoft.Json;

namespace HistoricalTrainApi.Models.HistoricService.ServiceDetails
{
    public class ServiceDetailsRequest
    {
        [JsonProperty("rid")]
        public string Rid { get; set; }
    }
}
