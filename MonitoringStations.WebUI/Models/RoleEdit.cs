using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MonitoringStations.WebUI.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUserCustom> Members { get; set; }
        public IEnumerable<IdentityUserCustom> NonMembers { get; set; }
    }
}