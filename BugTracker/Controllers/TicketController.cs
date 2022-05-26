using Microsoft.AspNetCore.Mvc;
using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }
        private TicketBusinessLogic TicketBL { get; set; }
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
        {
            db = context;
            TicketBL = new TicketBusinessLogic(new TicketRepository(context));
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            userManager = _userManager;
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

        public async Task<IActionResult> AssignDeveloper(int? id)
        {
            ViewBag.Id = id;
            var developers = await userManager.GetUsersInRoleAsync("Developer");
            ViewBag.developers = new SelectList(developers, "Id", "Email");
            return View(developers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignDeveloper(int? ticketId, string? userId)
        {
            Tickets ticket = TicketBL.GetTicket((int)ticketId);
            TicketStatuses status = db.TicketStatuses.First(s => s.Name == "Assigned");
            ApplicationUser user = db.Users.Find(userId);
            if (user != null && ticket != null)
            {
                ticket.AssignedToUser = user;
                ticket.AssignedToUserId = user.Id;
                ticket.TicketStatus = status;
                user.AssignedToTickets.Add(ticket);
                db.SaveChanges();
            }
            return RedirectToRoute(new { action = "Details", id = ticketId });
        }
    }
}
