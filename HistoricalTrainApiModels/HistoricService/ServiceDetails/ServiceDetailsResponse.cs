using Newtonsoft.Json;

namespace HistoricalTrainApiModels.HistoricService.ServiceDetails
{
    public class ServiceDetailsResponse
    {
        [JsonProperty("serviceAttributesDetails")]
        public ServiceAttributesDetails ServiceAttributesDetails { get; set; }
    }
}
