using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MonitoringStations.WebUI.Models;

namespace MonitoringStations.WebUI.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-user")]
    public class UserRolesTagHelper : TagHelper
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUserCustom> _userManager;

        public UserRolesTagHelper(UserManager<IdentityUserCustom> usermgr, RoleManager<IdentityRole> rolemgr)
        {
            _userManager = usermgr;
            _roleManager = rolemgr;
        }

        [HtmlAttributeName("i-user")] public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var names = new List<string>();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                foreach (var it in _roleManager.Roles.ToList())
                    if (it != null && await _userManager.IsInRoleAsync(user, it.Name))
                        names.Add(it.NormalizedName);
            }
            output.Content.SetContent(names.Count == 0 ? "NO ROLES" : string.Join(", ", names.ToList()));
        }
    }
}






//public RoleUsersTagHelper(UserManager<IdentityUserCustom> usermgr, RoleManager<IdentityRole> rolemgr)
//{
//    _userManager = usermgr;
//    _roleManager = rolemgr;
//}

//[HtmlAttributeName("i-role")] public string RoleId { get; set; }

//public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
//{
//    var names = new List<string>();
//    var role = await _roleManager.FindByIdAsync(RoleId);
//    if (role != null)
//        foreach (var it in _userManager.Users)
//            if (it != null && await _userManager.IsInRoleAsync(it, role.Name))
//                names.Add(it.Nick);
//    output.Content.SetContent(names.Count == 0 ? "NO USERS" : string.Join(", ", names));
//}