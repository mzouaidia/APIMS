using MonitoringStations.Domain.Models;

namespace MonitoringStations.Core.Interfaces
{
    public interface IApiTokenService
    {
        bool IsTokenExist(string token);
        
        void SaveApiToken(ApiToken token);
    }
}