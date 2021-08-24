using System;

namespace MonitoringStations.Domain.Dto
{
    public class StationDto
    {
        public long Id { get; set; }
        
        public string Hostname { get; set; }

        public string AddressIp { get; set; }

        public string StoreNumber { get; set; }

        public string MacAddress { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModify { get; set; }
    }
}