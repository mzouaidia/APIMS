namespace MonitoringStations.Domain.Models
{
    public class ExistResult
    {
        public bool IsExist { get; set; }

        public Station Station { get; set; } = null;
    }
}