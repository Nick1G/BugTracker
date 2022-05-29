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
    public class TicketController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }
        private TicketBusinessLogic TicketBL { get; set; }
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            TicketBL = new TicketBusinessLogic(new TicketRepository(context));
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? listType)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    ViewBag.User = "Admin";
                else if (await _userManager.IsInRoleAsync(user, "Project Manager"))
                    ViewBag.User = "Manager";
                else if (await _userManager.IsInRoleAsync(user, "Developer"))
                    ViewBag.User = "Dev";
                else if (await _userManager.IsInRoleAsync(user, "Submitter"))
                    ViewBag.User = "Submitter";
            }

            var ticketsList = TicketBL.AllTickets();

            switch (listType)
            {
                case "ProjectTickets":
                    ticketsList = TicketBL.GetTicketsList(ticket => ticket.Project.Users.Contains(user));
                    break;
                case "AssignedTickets":
                    ticketsList = TicketBL.GetAssignedTickets(user);
                    break;
                case "OwnedTickets":
                    ticketsList = TicketBL.GetOwnedTickets(user);
                    break;
            }

            return View(ticketsList);
        }

        [Authorize(Roles = "Submitter")]
        public async Task<IActionResult> Create(int? id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
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
