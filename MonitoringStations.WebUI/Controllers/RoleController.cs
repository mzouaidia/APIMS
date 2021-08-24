using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonitoringStations.WebUI.Models;

namespace MonitoringStations.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUserCustom> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUserCustom> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                Errors(result);
            }

            return View(name);
        }

        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<IdentityUserCustom>();
            var nonMembers = new List<IdentityUserCustom>();

            foreach (var it in _userManager.Users)
                if (await _userManager.IsInRoleAsync(it, role.Name))
                    members.Add(it);
                else
                    nonMembers.Add(it);

            var result = new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification roleModify)
        {
            IdentityResult result;
            IdentityUserCustom user;

            if (ModelState.IsValid)
            {
                foreach (var it in roleModify.AddIds ?? new string[] { })
                {
                    user = await _userManager.FindByIdAsync(it);

                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, roleModify.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }

                foreach (var it in roleModify.DeleteIds ?? new string[] { })
                {
                    user = await _userManager.FindByIdAsync(it);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, roleModify.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }

                return await Update(roleModify.RoleId);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                Errors(result);
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return View("Index", _roleManager.Roles);
        }

        private void Errors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}