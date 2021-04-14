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
            DateTime startDate,
            DateTime endDate,
            string startLocation,
            string endLocation,
            CancellationToken cancellationToken)
        {
            if( string.IsNullOrWhiteSpace(startLocation) || 
                string.IsNullOrWhiteSpace(endLocation))
            {
                return BadRequest();
            }

            IList<HistoricalRecord> results;
            try
            {
                results = await historicServiceRepository.GetTrainTimes(
                    startDate,
                    endDate,
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
