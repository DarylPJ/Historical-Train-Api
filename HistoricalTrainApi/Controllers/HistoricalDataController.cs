using HistoricalTrainApiRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
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
            [FromQuery]string date,
            [FromQuery]string from,
            [FromQuery]string to,
            CancellationToken cancellationToken)
        {
            if(!DateTime.TryParse(date, out var dateTime) || 
                string.IsNullOrWhiteSpace(from) || 
                string.IsNullOrWhiteSpace(to))
            {
                return BadRequest();
            }

            var results = await historicServiceRepository.GetTrainTimes(dateTime, from, to, cancellationToken);
            return Ok(results);
        }
    }
}
