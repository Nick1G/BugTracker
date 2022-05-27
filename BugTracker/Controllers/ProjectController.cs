using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext db;

        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            db = context;
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            _userManager = um;
        }

        [Authorize(Roles = "Admin, Project Manager, Developer, Submitter")]
        public async Task<IActionResult> Index(bool? allView)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin") || roles.Contains("Project Manager"))
                ViewBag.Manager = user;
            else
                ViewBag.Manager = null;

            if (allView == false || allView == null)
                return View(ProjectBL.GetAssignedProjects(user));
            else
                return View(ProjectBL.AllProjects());

        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject([Bind("Id, Name")] Projects project)
        {
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.First(u => u.Email == username);
            if (ModelState.IsValid)
            {
                user.Projects.Add(project);
                project.Users.Add(user);
                ProjectBL.CreateProject(project);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;

            Projects project = ProjectBL.GetProject((int)id);
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id, Name")] Projects project)
        {
            if(id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ProjectBL.UpdateProject(project);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;

            Projects project = ProjectBL.GetProject((int)id);
            return View(project);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Projects project = ProjectBL.GetProject((int)id);
            if(project != null)
            {
                ProjectBL.DeleteProject(project);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            Projects project = ProjectBL.GetProject((int)id);
            if(project != null)
            {
                return View(project);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult AssignUsers(int? Id)
        {
            ViewBag.projectId = Id;
            ViewBag.Users = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignUsers(int? projectId, string? userId)
        {
            ViewBag.projectId = projectId;
            ViewBag.Users = new SelectList(db.Users, "Id", "Email");
            Projects project = ProjectBL.GetProject((int)projectId);
            ApplicationUser user = db.Users.Find(userId);

            if(project != null && user != null)
            {
                project.Users.Add(user);
                user.Projects.Add(project);
                string message = "User " + user.Email + " successfully assigned to " + project.Name;
                db.SaveChanges();
                ViewBag.message = message;
            }
            return View("AssignUsers");
        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult RemoveUsers(int? Id)
        {
            ViewBag.projectId = Id;
            Projects project = ProjectBL.GetProject((int)Id);
            ViewBag.Users = new SelectList(project.Users, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveUsers(int? projectId, string? userId)
        {
            ViewBag.projectId = projectId;
            Projects project = ProjectBL.GetProject((int)projectId);
            ViewBag.Users = new SelectList(project.Users, "Id", "Email");
            ApplicationUser user = db.Users.Find(userId);

            if (project != null && user != null)
            {
                project.Users.Remove(user);
                user.Projects.Remove(project);
                string message = "User " + user.Email + " successfully removed from " + project.Name;
                db.SaveChanges();
                ViewBag.message = message;
            }
            return View("AssignUsers");
        }
    }
}
