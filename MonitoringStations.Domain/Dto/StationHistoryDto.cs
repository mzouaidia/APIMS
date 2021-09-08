using System;

namespace MonitoringStations.Domain.Dto
{
    public class StationHistoryDto
    {
        public long Id { get; set; }

        //public string HostName { get; set; }

        public string AddressIp { get; set; }

        public string StoreNumber { get; set; }

        public string PrinterName { get; set; }

        public string PrinterState { get; set; }

        public string PrinterInfo { get; set; }

        public DateTime CreateDate { get; set; }
    }
}