using Microsoft.AspNetCore.Identity;

namespace MonitoringStations.WebUI.Models
{
    public class IdentityUserCustom : IdentityUser
    {
        public string Nick { get; set; }
    }
}