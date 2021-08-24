using System.Collections.Generic;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Domain.Interfaces
{
    public interface IStationHistoryRepository
    {
        IEnumerable<StationHistory> GetHistoryStation(long id);

        IEnumerable<StationHistory> GetHistoryStation(string name);

        IEnumerable<StationHistory> GetHistoryStationByMacAddress(string macAddress);

        int InsertHistoryStation(StationHistory stationHistory);
    }
}