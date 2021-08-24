using System.Collections.Generic;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Domain.Interfaces
{
    public interface IStationRepository
    {
        IEnumerable<Station> GetStations();

        bool GetStationExist(string hostname, string macAddress, out Station station);

        int InsertStation(Station station);

        int UpdateStation(Station station);

        int DeleteStation(int stationId);

        int DeleteStation(Station station);
        
        string GetStationHostname(long stationId);
    }
}