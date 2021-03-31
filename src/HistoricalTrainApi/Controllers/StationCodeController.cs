using HistoricalTrainApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HistoricalTrainApi.Controllers
{
    [ApiController]
    [Route("StationCodes")]
    public class StationCodeController : Controller
    {
        private readonly IStationCodeRepository stationCodeRepository;

        public StationCodeController(IStationCodeRepository stationCodeRepository)
        {
            this.stationCodeRepository = stationCodeRepository ?? throw new ArgumentNullException(nameof(stationCodeRepository));
        }

        [HttpGet]
        public IActionResult Index() =>
            Ok(stationCodeRepository.GetStationCodes());
    }
}
