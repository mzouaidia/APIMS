﻿namespace MonitoringStations.Domain.Models
{
    public class AuthenticationJwt
    {
        public string JwtKey { get; set; }
        
        public int JwtExpireMinutes { get; set; }
        
        public string JwtIssuer { get; set; }
    }
}
