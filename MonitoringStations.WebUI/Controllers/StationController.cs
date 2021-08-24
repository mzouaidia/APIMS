using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MonitoringStations.Core.Interfaces;

namespace MonitoringStations.WebUI.Controllers
{
    [Authorize(Roles = "Support, Admin")]
    public class StationController : Controller
    {
        private readonly IStationService _stationService;

        public StationController(IStationService service)
        {
            _stationService = service;
        }

        public IActionResult Index()
        {
            var model = _stationService.GetStations();

            return View(model);
        }
    }
}