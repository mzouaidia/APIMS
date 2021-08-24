using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoringStations.Core.Interfaces;

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
            //var model = _stationService.GetHistoryById(id);
            
            //return View(model);
            return View();
        }

    }
}
