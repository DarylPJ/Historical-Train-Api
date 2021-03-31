using HistoricalTrainApi.Controllers;
using HistoricalTrainApi.Models;
using HistoricalTrainApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HistoricalTrainApi.Tests.Controllers
{
    public class HistoricalDataControllerTests
    {
        [Theory]
        [InlineData(null, "2021/03/30 11:00", "ABC", "DEF")]
        [InlineData(" ", "2021/03/30 11:00", "ABC", "DEF")]
        [InlineData("2021/03/30 10:00", " ", "ABC", "DEF")]
        [InlineData("2021/03/30 10:00", null, "ABC", "DEF")]
        [InlineData("2021/03/30 10:00", "2021/ 03/30 11:00", null, "DEF")]
        [InlineData("2021/03/30 10:00", "2021/03/30 11:00", " ", "DEF")]
        [InlineData("2021/03/30 10:00", "2021/03/30 11:00", "ABC", null)]
        [InlineData("2021/03/30 10:00", "2021/03/30 11:00", "ABC", " ")]
        [InlineData("2021/03/29 10:00", "2021/03/30 11:00", "ABC", "DEF")]
        [InlineData("not a datetime", "2021/03/30 11:00", "ABC", "DEF")]
        [InlineData("2021/03/30 10:00", "not a datetime", "ABC", "DEF")]
        public async Task GetData_WithInvalidParameters_ReturnsBadRequest(
            string startDate,
            string endDate,
            string startLocation,
            string endLocation)
        {
            var historicServiceRepository = Mock.Of<IHistoricServiceRepository>();

            var historicalDataController = new HistoricalDataController(historicServiceRepository);
            var result = await historicalDataController.GetDataAsync(
                startDate,
                endDate,
                startLocation,
                endLocation,
                CancellationToken.None);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task HistoricServiceThrowsHistoricServiceException_ReturnsServiceUnavailable()
        {
            const string startDate = "2021/03/30 10:00";
            const string endDate = "2021/03/30 11:00";
            const string startLocation = "ABC";
            const string endLocation = "DEF";

            var historicalRecords = (IList<HistoricalRecord>)new List<HistoricalRecord>();

            var mockHistoricServiceRepository = new Mock<IHistoricServiceRepository>();
            mockHistoricServiceRepository
                .Setup(i => i.GetTrainTimes(
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    startLocation,
                    endLocation,
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HistoricServiceException());

            var historicalDataController = new HistoricalDataController(mockHistoricServiceRepository.Object);
            var result = await historicalDataController.GetDataAsync(
                startDate,
                endDate,
                startLocation,
                endLocation,
                CancellationToken.None);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(503, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetData_WithValidParameters_ReturnsOk()
        {
            const string startDate = "2021/03/30 10:00";
            const string endDate = "2021/03/30 11:00";
            const string startLocation = "ABC";
            const string endLocation = "DEF";

            var historicalRecords = (IList<HistoricalRecord>) new List<HistoricalRecord>();

            var historicServiceRepository = Mock.Of<IHistoricServiceRepository>(i =>
                i.GetTrainTimes(
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    startLocation,
                    endLocation,
                    It.IsAny<CancellationToken>()) == Task.FromResult(historicalRecords));

            var historicalDataController = new HistoricalDataController(historicServiceRepository);
            var result = await historicalDataController.GetDataAsync(
                startDate,
                endDate,
                startLocation,
                endLocation,
                CancellationToken.None);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(historicalRecords, objectResult.Value);
        }
    }
}
