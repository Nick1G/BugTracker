using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }

        public ProjectController(ApplicationDbContext context)
        {
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
        }

        [Authorize(Roles = "Admin, Project Manager")]
        public IActionResult Index()
        {
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
            if (ModelState.IsValid)
            {
                ProjectBL.CreateProject(project);
            }
            return RedirectToAction(nameof(Index));
        }

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
    }
}
