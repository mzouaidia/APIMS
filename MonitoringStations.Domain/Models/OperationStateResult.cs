using MonitoringStations.Common.Enums;

namespace MonitoringStations.Domain.Models
{
    public class OperationStateResult
    {
        public OperationTypes OperationType { get; set; }

        public long RowId { get; set; }

        public string Msg { get; set; }
    }
}
