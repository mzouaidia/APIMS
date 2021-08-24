using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringStations.Domain.Models
{
    public class StationHistory
    {
        private DateTime _createDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        public string AddressIp { get; set; }

        [Required]
        public string StoreNumber { get; set; }

        [Required]
        public string PrinterName { get; set; }

        [Required]
        public string PrinterState { get; set; }

        [Required]
        public string PrinterInfo { get; set; }

        [Required]
        public DateTime CreateDate
        { 
            get => new DateTime(_createDate.Year, _createDate.Month, _createDate.Day, _createDate.Hour, _createDate.Minute, _createDate.Second);
            set => _createDate = value;
        }

        public Station Station { get; set; }
    }
}