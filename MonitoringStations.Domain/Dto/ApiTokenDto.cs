using System;
using System.ComponentModel.DataAnnotations;

namespace MonitoringStations.Domain.Dto
{
    public class ApiTokenDto
    {
        [Required]
        [MinLength(128)]
        public string Token { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime ExpireDate { get; set; }

        [Required]
        public bool IsExpire { get; set; }
    }
}
