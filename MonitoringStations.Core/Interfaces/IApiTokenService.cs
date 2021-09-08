using MonitoringStations.Domain.Dto;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.Interfaces
{
    public interface IApiTokenService
    {
        public string GenerateJwt(LoginDto loginDto);

        bool IsTokenExist(string token);
        
        void SaveApiToken(ApiToken token);
    }
}