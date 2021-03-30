using HistoricalTrainApiModels;
using HistoricalTrainApiModels.HistoricService.ServiceMetrics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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

        public Task<ServiceMetricsResponse> GetTrainTimes(DateTime date, string fromLocation, string toLocation, CancellationToken cancellationToken)
        {
            return GetServiceMetricsAsync(date, fromLocation, toLocation, cancellationToken);
        }

        private async Task<ServiceMetricsResponse> GetServiceMetricsAsync(DateTime date, string fromLocation, string toLocation, CancellationToken cancellationToken)
        {
            Days days = date.DayOfWeek switch
            {
                DayOfWeek.Saturday => Days.Saturday,
                DayOfWeek.Sunday => Days.Sunday,
                _ => Days.Weekday
            };

            var hour = GetTwoDigitValue(date.Hour);
            var minute = GetTwoDigitValue(date.Minute);

            var requestCotent = new ServiceMetricsRequest
            {
                FromLocation = fromLocation,
                ToLocation = toLocation,
                FromTime = $"{hour}{minute}",
                ToTime = $"{hour}{GetTwoDigitValue(date.Minute + 1)}",
                FromDate = date.ToString("yyyy-MM-dd"),
                ToDate = date.ToString("yyyy-MM-dd"),
                Days = days
            };
            var json = JsonConvert.SerializeObject(requestCotent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var client = clientFactory.CreateClient(DarwinClientName);

            using var response = await client.PostAsync(serviceUris.CurrentValue.ServiceMetrics, content, cancellationToken);
            var text = await response.Content.ReadAsStringAsync();
            var metricsResponse = JsonConvert.DeserializeObject<ServiceMetricsResponse>(text);

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
