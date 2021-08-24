using System.Linq;
using System.Collections.Generic;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Interfaces;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.DB.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly DataContext _context;

        public StationRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Station> GetStations()
        {
            return _context.Stations;
        }
        
        public string GetStationHostname(long stationId)
        {
            return _context.Stations.FirstOrDefault(x => x.Id == stationId)?.Hostname ?? string.Empty;
        }

        public bool GetStationExist(string hostname, string macAddress, out Station station)
        {
            var result = _context.Stations.FirstOrDefault(x => x.Hostname == hostname && x.MacAddress == macAddress);

            station = result ?? new Station();

            return result != null;
        }

        public int InsertStation(Station station)
        {
            var result = (int) _context.Stations.Add(station).State;
            _context.SaveChanges();

            return result;
        }

        public int DeleteStation(int stationId)
        {
            var result = (int) _context.Stations.Remove(_context.Stations.FirstOrDefault(x => x.Id == stationId)).State;
            _context.SaveChanges();

            return result;
        }

        public int DeleteStation(Station station)
        {
            var result = (int) _context.Stations.Remove(station).State;
            _context.SaveChanges();

            return result;
        }

        public int UpdateStation(Station station)
        {
            var result = (int) _context.Stations.Update(station).State;
            _context.SaveChanges();

            return result;
        }

    }
}