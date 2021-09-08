using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MonitoringStations.WebUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace MonitoringStations.WebUI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUserCustom> _userManager;

        public UserController(UserManager<IdentityUserCustom> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.OrderBy(x=>x.UserName).ToList());
        }
    }
}
