using System.Collections.Generic;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.ViewModels
{
    public class StationHistoryViewModel
    {
        public string StationHostName { get; set; }

        public IEnumerable<StationHistory> StationHistories { get; set; }
    }
}