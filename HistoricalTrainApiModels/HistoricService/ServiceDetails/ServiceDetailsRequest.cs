using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceDetails
{
    public class ServiceDetailsRequest
    {
        [JsonProperty("rid")]
        public string Rid { get; set; }
    }
}
