using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(ApplicationDbContext context, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            db = context;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddUserToRole()
        {
            ViewBag.Roles = new SelectList(roleManager.Roles);
            var allUsers = db.Users.ToList();
            ViewBag.Users = new SelectList(allUsers, "Id", "Email");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(string? userId, string? role)
        {
            string message;
            try
            {
                if (userId != null && role != null)
                {
                    ApplicationUser user = (ApplicationUser)await userManager.FindByIdAsync(userId);
                    bool roleExists = await roleManager.RoleExistsAsync(role);
                    ViewBag.Roles = new SelectList(roleManager.Roles);
                    var allUsers = db.Users.ToList();
                    ViewBag.Users = new SelectList(allUsers, "Id", "Email");


                    if (roleExists && user != null)
                    {
                        bool userIsAlreadyInRole = await userManager.IsInRoleAsync(user, role);
                        if (!userIsAlreadyInRole)
                        {
                            await userManager.AddToRoleAsync(user, role);
                            db.SaveChanges();
                            message = "User " + user.Email + " successfully set as " + role;
                            ViewBag.Message = message;
                        }
                        else
                        {
                            message = "User " + user.Email + " is already in role " + role;
                            ViewBag.Message = message;
                        }
                    }
                }
                return View("AddUserToRole");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult RemoveUserFromRole()
        {
            ViewBag.Roles = new SelectList(roleManager.Roles);
            var allUsers = db.Users.ToList();
            ViewBag.Users = new SelectList(allUsers, "Id", "Email");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(string? userId, string? role)
        {
            string message;
            try
            {
                if (userId != null && role != null)
                {
                    ApplicationUser user = (ApplicationUser)await userManager.FindByIdAsync(userId);
                    bool roleExists = await roleManager.RoleExistsAsync(role);
                    ViewBag.Roles = new SelectList(roleManager.Roles);
                    var allUsers = db.Users.ToList();
                    ViewBag.Users = new SelectList(allUsers, "Id", "Email");


                    if (roleExists && user != null)
                    {
                        bool userIsAlreadyInRole = await userManager.IsInRoleAsync(user, role);
                        if (userIsAlreadyInRole)
                        {
                            await userManager.RemoveFromRoleAsync(user, role);
                            db.SaveChanges();
                            message = "User " + user.Email + " successfully removed from " + role;
                            ViewBag.Message = message;
                        }
                        else
                        {
                            message = "User " + user.Email + " is not in role " + role;
                            ViewBag.Message = message;
                        }
                    }
                }
                return View("RemoveUserFromRole");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
