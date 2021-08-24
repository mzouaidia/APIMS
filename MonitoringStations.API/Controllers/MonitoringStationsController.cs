using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MonitoringStations.Common.Enums;
using MonitoringStations.Core.Interfaces;
using MonitoringStations.Domain.Dto;
using MonitoringStations.API.Filters;
using Microsoft.AspNetCore.Authorization;

namespace MonitoringStations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    //[ServiceFilter(typeof(TokenAuthenticationFilter))]
    public class MonitoringStationsController : ControllerBase
    {
        private readonly IStationService _stationService;

        public MonitoringStationsController(IStationService services)
        {
            _stationService = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationDto>>> GetStations()
        {
            var stations = await _stationService.GetStations();
            if (stations is null) return NotFound();

            return Ok(stations);
        }

        [HttpPost]
        public async Task<ActionResult> SetStatus([FromBody] InputDataStationDto inputDataStationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var state = await _stationService.InsertUpdateStation(inputDataStationDto);

            if (state.OperationType == OperationTypes.Insert)
                return Created(string.Empty, state.RowId.ToString());

            return Ok(state.RowId);
        }

        //[HttpPost("Status")]
        //public JsonResult Post([FromBody] InputDataStationDto inputData)
        //{
        //    try
        //    {
        //        var state = _stationService.InsertUpdateStation(inputData);

        //        return new JsonResult(((EntityState)state).ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        new EventLogger().WriteLog(ex.Message);

        //        return new JsonResult(ex.Message + "\n" + ex.InnerException?.Message);
        //    }
        //}
    }
}