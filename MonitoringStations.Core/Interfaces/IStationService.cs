using System.Collections.Generic;
using System.Threading.Tasks;
using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.Interfaces
{
    public interface IStationService
    {
        #region Stations
        Task<IEnumerable<StationDto>> GetStations();

        bool GetStationExist(string hostname, string macAddress, out StationDto station);

        string GetStationHostname(long stationId);

        string GetStationMac(long stationId);

        Task<OperationStateResult> InsertUpdateStation(InputDataStationDto inputDataStationDto);

        int DeleteStation(long stationId);

        int DeleteStation(Station station);
        #endregion

        #region Stations History

        Task<IEnumerable<StationHistoryDto>> GetHistoryStation(long id);

        #endregion

        //Task<int> InsertStation(Station station);

        //Task<int> UpdateStation(Station station);
    }
}