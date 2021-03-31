using DeepEqual.Syntax;
using HistoricalTrainApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HistoricalTrainApi.IntegrationTests
{
    public class HistoricDataIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        const string serviceMetricsUri = "https://hsp-prod.rockshore.net/api/v1/serviceMetrics";
        const string serviceDetailsUri = "https://hsp-prod.rockshore.net/api/v1/serviceDetails";

        private readonly WebApplicationFactory<Startup> factory;

        public HistoricDataIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        private HttpClient GetTestClientwithExternalclientMocked(HttpClient externalHttpClient)
        {
            var httpClientFactory = Mock.Of<IHttpClientFactory>(
                i => i.CreateClient(It.IsAny<string>()) == externalHttpClient);

            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IHttpClientFactory>(httpClientFactory);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task ExternalHistoricServiceMetricsDown_ReturnsServiceUnavailable()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(serviceMetricsUri)
                .Respond(HttpStatusCode.InternalServerError);

            var client = GetTestClientwithExternalclientMocked(mockHttp.ToHttpClient());

            var response = await client.GetAsync("/HistoricalData?startLocation=CFL&EndLocation=LDS&startDate=2021/03/25 07:00&endDate=2021/03/25 07:30");

            Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
        }

        [Fact]
        public async Task ExternalHistoricServiceDetailsDown_ReturnsServiceUnavailable()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(serviceMetricsUri)
                .Respond("application/json", JsonConvert.SerializeObject(HistoricalServiceMockData.GetServiceMetricsResponse()));

            var serviceDetailsRequest = mockHttp.When(serviceDetailsUri)
                .Respond(HttpStatusCode.InternalServerError);

            var client = GetTestClientwithExternalclientMocked(mockHttp.ToHttpClient());

            var response = await client.GetAsync("/HistoricalData?startLocation=CFL&EndLocation=LDS&startDate=2021/03/25 07:00&endDate=2021/03/25 07:30");

            Assert.Equal(503, (int)response.StatusCode);
            Assert.Equal(1, mockHttp.GetMatchCount(serviceDetailsRequest));
        }

        [Fact]
        public async Task ExternalHistoricServiceWorking_ExpectedDataReturned()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(serviceMetricsUri)
                .Respond("application/json",
                JsonConvert.SerializeObject(HistoricalServiceMockData.GetServiceMetricsResponse()));

            mockHttp.When(serviceDetailsUri)
                .Respond("application/json",
                JsonConvert.SerializeObject(HistoricalServiceMockData.GetServiceDetailsResponse()));

            var client = GetTestClientwithExternalclientMocked(mockHttp.ToHttpClient());

            var response = await client.GetAsync("/HistoricalData?startLocation=CFL&EndLocation=LDS&startDate=2021/03/25 07:00&endDate=2021/03/25 07:30");

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var actualResponse = JsonConvert.DeserializeObject<IList<HistoricalRecord>>(content);

            actualResponse.ShouldDeepEqual(HistoricalServiceMockData.GetExpectedApiResponse());
        }
    }
}
