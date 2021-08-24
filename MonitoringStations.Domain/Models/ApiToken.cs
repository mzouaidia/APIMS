using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringStations.Domain.Models
{
    public class ApiToken
    {
        private DateTime _createDate;
        private DateTime _expireDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreateDate
        {
            get => new(_createDate.Year, _createDate.Month, _createDate.Day, _createDate.Hour, _createDate.Minute, _createDate.Second);
            set => _createDate = value;
        }

        public DateTime ExpireDate
        {
            get => new(_expireDate.Year, _expireDate.Month, _expireDate.Day, _expireDate.Hour, _expireDate.Minute, _expireDate.Second);
            set => _expireDate = value;
        }

        [Required]
        public bool IsExpire { get; set; }
    }
}
