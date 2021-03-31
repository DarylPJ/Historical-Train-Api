using HistoricalTrainApi.Models;
using HistoricalTrainApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HistoricalTrainApi.Controllers
{
    [ApiController]
    [Route("HistoricalData")]
    public class HistoricalDataController : Controller
    {
        private readonly IHistoricServiceRepository historicServiceRepository;

        public HistoricalDataController(IHistoricServiceRepository historicServiceRepository)
        {
            this.historicServiceRepository = historicServiceRepository ?? throw new ArgumentNullException(nameof(historicServiceRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetDataAsync(
            [FromQuery]string startDate,
            [FromQuery]string endDate,
            [FromQuery]string startLocation,
            [FromQuery]string endLocation,
            CancellationToken cancellationToken)
        {
            if(!DateTime.TryParse(startDate, out var startDateTime) ||
                !DateTime.TryParse(endDate, out var endDateTime) ||
                string.IsNullOrWhiteSpace(startLocation) || 
                string.IsNullOrWhiteSpace(endLocation) || 
                startDateTime.Date != endDateTime.Date)
            {
                return BadRequest();
            }

            IList<HistoricalRecord> results;
            try
            {
                results = await historicServiceRepository.GetTrainTimes(
                    startDateTime,
                    endDateTime,
                    startLocation,
                    endLocation,
                    cancellationToken);
            }
            catch (HistoricServiceException)
            {
                return StatusCode(503, "Error with National Rail Darwin data feeds. Please try again later.");
            }

            return Ok(results);
        }
    }
}
