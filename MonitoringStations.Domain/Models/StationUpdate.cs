using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringStations.Domain.Models
{
    public class StationUpdate
    {
        private DateTime _createDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long Id { get; set; }

        [Required]
        public string CurrentVersion { get; set; }

        [Required]
        public string PreviousVersion { get; set; }

        [Required]
        public DateTime LastUpdateDate
        {
            get => new(_createDate.Year, _createDate.Month, _createDate.Day, _createDate.Hour, _createDate.Minute, _createDate.Second);
            set => _createDate = value;
        }

        [Required]
        [DefaultValue(false)]
        public bool ToUodate { get; set; }

        public long StationId { get; set; }

        public Station Station { get; set; }
    }
}
