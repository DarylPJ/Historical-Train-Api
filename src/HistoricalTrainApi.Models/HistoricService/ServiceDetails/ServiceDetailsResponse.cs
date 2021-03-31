using Newtonsoft.Json;

namespace HistoricalTrainApi.Models.HistoricService.ServiceDetails
{
    public class ServiceDetailsResponse
    {
        [JsonProperty("serviceAttributesDetails")]
        public ServiceAttributesDetails ServiceAttributesDetails { get; set; }
    }
}
