using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoringStations.Core.Interfaces;
using MonitoringStations.Core.ViewModels;
using System.Linq;

namespace MonitoringStations.WebUI.Controllers
{
    [Authorize(Roles = "Support, Admin")]
    public class StationHistoryController : Controller
    {
        private readonly IStationService _stationService;

        public StationHistoryController(IStationService service)
        {
            _stationService = service;
        }

        [Route("StationHistory/{id}")]
        public IActionResult Index(long id)
        {
            var viewModel = new StationHistoryViewModel
            {
                StationHostName = _stationService.GetStationHostname(id),
                MacAddress = _stationService.GetStationMac(id),
                StationHistories = _stationService.GetHistoryStation(id).Result.ToList()
            };
            
            return View(viewModel);
        }

    }
}
