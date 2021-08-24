using System.Collections.Generic;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.ViewModels
{
    public class StationsViewModel
    {
        public IEnumerable<Station> Stations { get; set; }
    }
}