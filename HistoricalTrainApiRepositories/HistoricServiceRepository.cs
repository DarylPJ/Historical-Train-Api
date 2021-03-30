using HistoricalTrainApiModels;
using HistoricalTrainApiModels.HistoricService.ServiceDetails;
using HistoricalTrainApiModels.HistoricService.ServiceMetrics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApiRepositories
{
    public class HistoricServiceRepository : IHistoricServiceRepository
    {
        const string DarwinClientName = "darwin";

        private readonly IHttpClientFactory clientFactory;
        private readonly IOptionsMonitor<HistoricServiceUris> serviceUris;

        public HistoricServiceRepository(
            IHttpClientFactory clientFactory,
            IOptionsMonitor<HistoricServiceUris> options)
        {
            this.clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            this.serviceUris = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IList<LocationDetail>> GetTrainTimes(
            DateTime startDate,
            DateTime endDate,
            string fromLocation,
            string toLocation,
            CancellationToken cancellationToken)
        {
            var serviceMetrics = await GetServiceMetricsAsync(startDate, endDate, fromLocation, toLocation, cancellationToken);
            var rids = serviceMetrics.Services.SelectMany(i => i.ServiceAttributesMetrics.Rids);

            var serviceDetails = new List<ServiceDetailsResponse>();
            foreach (var rid in rids)
            {
                serviceDetails.Add(await GetServiceDetailsAsync(rid, cancellationToken));
            }

            var serviceDetailsForStations = serviceDetails.SelectMany(i => i.ServiceAttributesDetails.Locations)
                .Where(i => string.Equals(i.Location, fromLocation) || string.Equals(i.Location, toLocation));

            return serviceDetailsForStations.ToList();
        }

        private Task<ServiceDetailsResponse> GetServiceDetailsAsync(string rid, CancellationToken cancellationToken)
        {
            var request = new ServiceDetailsRequest
            {
                Rid = rid
            };

            return PostContent<ServiceDetailsRequest, ServiceDetailsResponse>(
                request,
                serviceUris.CurrentValue.ServiceDetails,
                cancellationToken);
        }

        private Task<ServiceMetricsResponse> GetServiceMetricsAsync(
            DateTime startDate,
            DateTime endDate,
            string fromLocation,
            string toLocation,
            CancellationToken cancellationToken)
        {
            Days days = startDate.DayOfWeek switch
            {
                DayOfWeek.Saturday => Days.Saturday,
                DayOfWeek.Sunday => Days.Sunday,
                _ => Days.Weekday
            };

            var requestCotent = new ServiceMetricsRequest
            {
                FromLocation = fromLocation,
                ToLocation = toLocation,
                FromTime = $"{GetTwoDigitValue(startDate.Hour)}{GetTwoDigitValue(startDate.Minute)}",
                ToTime = $"{GetTwoDigitValue(endDate.Hour)}{GetTwoDigitValue(endDate.Minute)}",
                FromDate = startDate.ToString("yyyy-MM-dd"),
                ToDate = endDate.ToString("yyyy-MM-dd"),
                Days = days
            };

            return PostContent<ServiceMetricsRequest, ServiceMetricsResponse>(
                requestCotent,
                serviceUris.CurrentValue.ServiceMetrics,
                cancellationToken);
        }

        private async Task<Y> PostContent<T, Y>(T content, Uri uri, CancellationToken cancellationToken)
        {
            var json = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = clientFactory.CreateClient(DarwinClientName);

            using var response = await client.PostAsync(uri, stringContent, cancellationToken);
            var text = await response.Content.ReadAsStringAsync();
            var metricsResponse = JsonConvert.DeserializeObject<Y>(text);

            return metricsResponse;
        }

        private string GetTwoDigitValue(int value)
        {
            var stringVal = value.ToString();

            if(stringVal.Length == 1)
            {
                stringVal = $"0{stringVal}";
            }

            return stringVal;
        }
    }
}
