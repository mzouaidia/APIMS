using Microsoft.Extensions.DependencyInjection;
using MonitoringStations.Core.Interfaces;
using MonitoringStations.Core.Services;
using MonitoringStations.DB.Repositories;
using MonitoringStations.Domain.Interfaces;

namespace MonitoringStations.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IStationService, StationService>();

            services.AddScoped<IStationRepository, StationRepository>();

            //services.AddScoped<IStationHistoryRepository, StationHistoryRepository>();
        }
    }
}