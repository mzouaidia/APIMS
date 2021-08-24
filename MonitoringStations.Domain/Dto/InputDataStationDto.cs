using System.ComponentModel.DataAnnotations;

namespace MonitoringStations.Domain.Dto
{
    public class InputDataStationDto
    {
        [Required]
        [MinLength(15)]
        public string Hostname { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(15)]
        public string AddressIp { get; set; }

        [Required]
        public string StoreNumber { get; set; }

        [Required]
        [MinLength(12)]
        [MaxLength(17)]
        public string MacAddress { get; set; }

        [Required]
        public string PrinterName { get; set; }

        [Required]
        public string PrinterState { get; set; }

        [Required]
        public string PrinterInfo { get; set; }

        [MaxLength(10)]
        public string Zone { get; set; } = "";
    }
}