using Microsoft.AspNetCore.Mvc;
using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }
        private TicketBusinessLogic TicketBL { get; set; }
        private readonly ApplicationDbContext db;

        public TicketController(ApplicationDbContext context)
        {
            db = context;
            TicketBL = new TicketBusinessLogic(new TicketRepository(context));
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
        }

        public IActionResult Index()
        {
            return View(TicketBL.AllTickets());
        }

        [Authorize(Roles = "Submitter")]
        public IActionResult Create(int? id)
        {
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Email == username);
            TicketStatuses status = db.TicketStatuses.First(s => s.Name == "Unassigned");
            ViewBag.Status = status.Id;
            ViewBag.UserId = user.Id;
            ViewBag.TicketType = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketPriorities = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.projectId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Description, Created, ProjectId, TicketTypeId, TicketPriorityId, TicketStatusId, OwnerUserId")]Tickets ticket)
        {
            if (ModelState.IsValid)
            {
                TicketBL.CreateTicket(ticket);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            Tickets ticket = TicketBL.GetTicket((int)id);
            if (ticket != null)
            {
                return View(ticket);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
