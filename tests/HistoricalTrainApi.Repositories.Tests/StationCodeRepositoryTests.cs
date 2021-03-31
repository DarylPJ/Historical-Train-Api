using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HistoricalTrainApi.Repositories.Tests
{
    public class StationCodeRepositoryTests
    {
        [Fact]
        public void CanGetStationCodes()
        {
            var mockStationCodes = new Dictionary<string, string>
            {
                ["this place"] = "abc"
            };

            var repository = new StationCodeRepository(mockStationCodes);

            var results = repository.GetStationCodes();

            Assert.Single(results);
            Assert.Equal(mockStationCodes, results);
        }
    }
}
