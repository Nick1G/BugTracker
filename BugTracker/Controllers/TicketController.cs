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
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext db;

        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            TicketBL = new TicketBusinessLogic(new TicketRepository(context), new TicketCommentRepository(context));
            ProjectBL = new ProjectBusinessLogic(new ProjectRepository(context));
            _userManager = userManager;
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

        public IActionResult CreateComment()
        {

            ViewBag.Tickets = new SelectList(TicketBL.GetTicketsList(_ => true), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment([Bind("Comment,TicketId")]TicketComments Comment)
        {
            var user = _userManager.Users.First(u => u.UserName == User.Identity.Name);
            Comment.Created = DateTime.Now;
            Comment.UserId = user.Id;

            TicketBL.Comment(Comment);

            return RedirectToAction(nameof(Index));

        }
    }
}
