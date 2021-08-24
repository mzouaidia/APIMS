using System.Collections.Generic;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Domain.Dto
{
    public class StationHistoryDto
    {
        public string StationHostName { get; set; }

        public IEnumerable<StationHistory> StationHistories { get; set; }
    }
}