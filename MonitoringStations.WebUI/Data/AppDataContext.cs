using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonitoringStations.WebUI.Models;

namespace MonitoringStations.WebUI.Data
{
    public class AppDataContext : IdentityDbContext<IdentityUserCustom>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }
    }
}