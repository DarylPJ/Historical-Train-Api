using HistoricalTrainApi.Models;
using HistoricalTrainApi.Models.HistoricService.ServiceDetails;
using HistoricalTrainApi.Models.HistoricService.ServiceMetrics;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HistoricalTrainApi.Repositories.Tests
{
    public class HistoricServiceRepositoryTests
    {
        private const string darwinClientName = "darwin";
        private readonly Uri serviceDetailsUrl = new Uri("https://serviceDetails.com");
        private readonly Uri serviceMetricsUrl = new Uri("https://serviceMetrics.com");

        [Fact]
        public async Task WhenHistoricServiceCallIsNotSuccessful_ThrowsHistoricServiceException()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://service*")
                .Respond(HttpStatusCode.BadRequest);

            var mockclient = mockHttp.ToHttpClient();

            var clientFactory = Mock.Of<IHttpClientFactory>(i => i.CreateClient(darwinClientName) == mockclient);
            var options = Mock.Of<IOptionsMonitor<HistoricServiceUris>>(i => i.CurrentValue == new HistoricServiceUris
            {
                ServiceDetails = serviceDetailsUrl,
                ServiceMetrics = serviceMetricsUrl
            });

            var repository = new HistoricServiceRepository(clientFactory, options);

            var getTrainTimesTask = repository.GetTrainTimes(DateTime.Now, DateTime.Now, "ABC", "DEF", CancellationToken.None);

            await Assert.ThrowsAsync<HistoricServiceException>(() => getTrainTimesTask);
        }

        [Fact]
        public async Task BuildsServiceMetricsRequestSuccessfully()
        {
            const string startLocation = "TND";
            const string endLocation = "OFM";
            var startDate = new DateTime(2021, 03, 31, 5, 3, 0);
            var endDate = new DateTime(2021, 03, 31, 5, 20, 0);

            var expectedRequest = new ServiceMetricsRequest
            {
                FromLocation = startLocation,
                ToLocation = endLocation,
                FromTime = "0503",
                ToTime = "0520",
                FromDate = "2021-03-31",
                ToDate = "2021-03-31",
                Days = Days.Weekday
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(serviceMetricsUrl.ToString())
                .WithContent(JsonConvert.SerializeObject(expectedRequest))
                .Respond("application/json", "{}");

            var mockclient = mockHttp.ToHttpClient();

            var clientFactory = Mock.Of<IHttpClientFactory>(i => i.CreateClient(darwinClientName) == mockclient);
            var options = Mock.Of<IOptionsMonitor<HistoricServiceUris>>(i => i.CurrentValue == new HistoricServiceUris
            {
                ServiceDetails = serviceDetailsUrl,
                ServiceMetrics = serviceMetricsUrl
            });

            var repository = new HistoricServiceRepository(clientFactory, options);

            await repository.GetTrainTimes(
                startDate,
                endDate,
                startLocation,
                endLocation,
                CancellationToken.None);
        }

        [Fact]
        public async Task CallsGetServiceDetailsEndpoint_ForEachRid()
        {
            const string startLocation = "TND";
            const string endLocation = "OFM";
            var startDate = new DateTime(2021, 03, 31, 5, 3, 0);
            var endDate = new DateTime(2021, 03, 31, 5, 20, 0);

            var serviceMetricsResponse = new ServiceMetricsResponse
            {
                Services =
                {
                    new Service
                    {
                        ServiceAttributesMetrics = new ServiceAttributesMetrics
                        {
                            Rids = {"123", "456"}
                        }
                    },
                    new Service
                    {
                        ServiceAttributesMetrics = new ServiceAttributesMetrics
                        {
                            Rids = {"789"}
                        }
                    }
                }
            };

            var serviceDetailsResponse = new ServiceDetailsResponse
            {
                ServiceAttributesDetails = new ServiceAttributesDetails
                {
                    Locations =
                    {
                        new LocationDetail
                        {
                            Location = startLocation
                        },
                        new LocationDetail
                        {
                            Location = endLocation
                        }
                    }
                }
            };

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(serviceMetricsUrl.ToString())
                .Respond("application/json", JsonConvert.SerializeObject(serviceMetricsResponse));

            var serviceDetailsRequest = mockHttp.When(serviceDetailsUrl.ToString())
                .Respond("application/json", JsonConvert.SerializeObject(serviceDetailsResponse));

            var mockclient = mockHttp.ToHttpClient();

            var clientFactory = Mock.Of<IHttpClientFactory>(i => i.CreateClient(darwinClientName) == mockclient);
            var options = Mock.Of<IOptionsMonitor<HistoricServiceUris>>(i => i.CurrentValue == new HistoricServiceUris
            {
                ServiceDetails = serviceDetailsUrl,
                ServiceMetrics = serviceMetricsUrl
            });

            var repository = new HistoricServiceRepository(clientFactory, options);

            await repository.GetTrainTimes(
                startDate,
                endDate,
                startLocation,
                endLocation,
                CancellationToken.None);

            Assert.Equal(3, mockHttp.GetMatchCount(serviceDetailsRequest));
        }
    }
}
