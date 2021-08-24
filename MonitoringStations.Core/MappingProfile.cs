using AutoMapper;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Station, StationDto>();
            CreateMap<InputDataStationDto, Station>();
            CreateMap<InputDataStationDto, StationHistory>();
            CreateMap<ApiTokenDto, ApiToken>();
        }
    }
}
