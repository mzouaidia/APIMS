using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringStations.Domain.Models
{
    public class Station
    {
        private DateTime _createDate;
        private DateTime _lastModify;
        private string _macAddress;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long Id { get; set; }

        [Required]
        public string Hostname { get; set; }

        [Required]
        public string AddressIp { get; set; }

        [Required]
        public string StoreNumber { get; set; }

        [Required]
        public string MacAddress
        {
            get => _macAddress;
            set => _macAddress = value?.Replace(":", string.Empty).Replace("-", string.Empty) ?? string.Empty;
        }

        public DateTime CreateDate
        {
            get => new (_createDate.Year, _createDate.Month, _createDate.Day, _createDate.Hour, _createDate.Minute, _createDate.Second);
            set => _createDate = value;
        }

        [Required]
        public DateTime LastModify
        {
            get => new (_lastModify.Year, _lastModify.Month, _lastModify.Day, _lastModify.Hour, _lastModify.Minute, _lastModify.Second);
            set => _lastModify = value;
        }

        [Required]
        [DefaultValue("")]
        public string Zone { get; set; }

        public ICollection<StationHistory> StationHistories { get; set; }
    }
}