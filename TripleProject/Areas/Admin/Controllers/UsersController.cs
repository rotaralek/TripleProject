using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.ViewModels;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Get: Admin/Users
        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _userManager.Users.CountAsync();
            var applicationDbContext = await _userManager.Users.Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public IActionResult Create()
        {
            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "NormalizedName", "Name");

            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,PhoneNumber,RoleName,Password,ConfirmPassword")] UserRoleViewModel userRole)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = userRole.UserName,
                    Email = userRole.Email,
                    PhoneNumber = userRole.PhoneNumber
                };

                IdentityResult result = await _userManager.CreateAsync(user, userRole.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userRole.RoleName);
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(userRole.RoleName, "true"));

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "NormalizedName", "Name", userRole.RoleName);

            return View(userRole);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var newRole = "";

            if (user == null)
            {
                return NotFound();
            }

            if (roles == null)
            {
                return NotFound();
            }

            foreach (var role in roles)
            {
                newRole = role;
            }

            UserRoleViewModel userRole = new UserRoleViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "NormalizedName", "Name", newRole);

            return View(userRole);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber,RoleName,Password,ConfirmPassword")] UserRoleViewModel userRole)
        {
            if (id != userRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var User = await _userManager.FindByIdAsync(userRole.Id);
                    User.UserName = userRole.UserName;
                    User.Email = userRole.Email;
                    User.PhoneNumber = userRole.PhoneNumber;
                    var result = await _userManager.UpdateAsync(User);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First().ToString());
                        return View();
                    }

                    var roles = await _userManager.GetRolesAsync(User);
                    foreach (var role in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(User, role);
                    }

                    var claims = await _userManager.GetClaimsAsync(User);
                    foreach (var claim in claims)
                    {
                        await _userManager.RemoveClaimAsync(User, claim);
                    }

                    await _userManager.AddToRoleAsync(User, userRole.RoleName);
                    await _userManager.AddClaimAsync(User, new System.Security.Claims.Claim(userRole.RoleName, "true"));

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            ViewData["RoleName"] = new SelectList(_roleManager.Roles, "NormalizedName", "Name", userRole.RoleName);

            return View(userRole);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}