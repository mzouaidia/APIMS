using System.Linq;
using System.Collections.Generic;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.DB.Repositories
{
    public class StationHistoryRepository
    {
        private readonly DataContext _context;

        public StationHistoryRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<StationHistory> GetHistoryStationByMacAddress(string macAddress)
        {
            return _context.StationHistory.Where(x => x.Station.MacAddress == macAddress).ToList();
        }

        public IEnumerable<StationHistory> GetHistoryStation(long id)
        {
            return _context.StationHistory.Where(x => x.Station.Id == id).ToList();
        }

        public IEnumerable<StationHistory> GetHistoryStation(string name)
        {
            return _context.StationHistory.Where(x => x.Station.Hostname == name).ToList();
        }

        public int InsertHistoryStation(StationHistory stationHistory)
        {
            if (stationHistory == null) return -1;

            var result = (int) _context.StationHistory.Add(stationHistory).State;
            _context.SaveChanges();

            return result;
        }
    }
}