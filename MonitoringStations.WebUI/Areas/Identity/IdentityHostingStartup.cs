using Microsoft.AspNetCore.Hosting;
using MonitoringStations.WebUI.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace MonitoringStations.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}