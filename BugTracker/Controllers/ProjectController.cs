using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
