using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Common.Enums;
using MonitoringStations.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MonitoringStations.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MonitoringStationsController : ControllerBase
    {
        private readonly IStationService _stationService;
        private readonly IApiTokenService _apiTokenService;

        public MonitoringStationsController(IStationService services, IApiTokenService apiTokenService)
        {
            _stationService = services;
            _apiTokenService = apiTokenService;
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody]LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var token = _apiTokenService.GenerateJwt(loginDto);

                var result = new TokenResultDto
                {
                    Status = "OK",
                    Msg = "Success",
                    Key = token
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                var result = new TokenResultDto
                {
                    Status = "Fail",
                    Msg = ex.Message,
                    Key = string.Empty
                };

                return BadRequest(result);
            }
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