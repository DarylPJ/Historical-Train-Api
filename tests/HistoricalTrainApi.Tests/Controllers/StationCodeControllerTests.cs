using HistoricalTrainApi.Controllers;
using HistoricalTrainApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HistoricalTrainApi.Tests.Controllers
{
    public class StationCodeControllerTests
    {
        [Fact]
        public void OkayObjectResultReturned()
        {
            var stationCodeRepository = Mock.Of<IStationCodeRepository>();

            var stationCodeController = new StationCodeController(stationCodeRepository);

            var results = stationCodeController.Index();

            Assert.IsType<OkObjectResult>(results);
        }
    }
}
