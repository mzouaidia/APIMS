using System.Collections.Generic;
using System.Threading.Tasks;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.Interfaces
{
    public interface IStationService
    {
        int DeleteStation(long stationId);

        int DeleteStation(Station station);
        
        bool GetStationExist(string hostname, string macAddress, out StationDto station);
        
        string GetStationHostname(long stationId);

        Task<IEnumerable<StationDto>> GetStations();

        Task<OperationStateResult> InsertUpdateStation(InputDataStationDto inputDataStationDto);

        //Task<int> InsertStation(Station station);

        //Task<int> UpdateStation(Station station);
    }
}