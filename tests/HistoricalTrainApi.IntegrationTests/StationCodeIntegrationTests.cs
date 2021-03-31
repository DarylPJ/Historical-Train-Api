using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace HistoricalTrainApi.IntegrationTests
{
    public class StationCodeIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public StationCodeIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task StationCodeDataReturned()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/StationCodes");
            response.EnsureSuccessStatusCode();

            var stations = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(stations);
        }
    }
}
