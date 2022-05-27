using Microsoft.AspNetCore.Mvc;
using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Models;
using BugTracker.Models.AssessmentClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BugTracker.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private ProjectBusinessLogic ProjectBL { get; set; }
        private TicketBusinessLogic TicketBL { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
        {
            db = context;
            TicketBL = new TicketBusinessLogic(new TicketRepository(context)/*, new TicketCommentRepository(context)*/);
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

        public IActionResult CreateComment(int? id)
        {
            ViewBag.TicketId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateComment(string comment, int ticketId)
        {
            Tickets ticket = TicketBL.GetTicket(ticketId);
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.First(u => u.Email == username);
            TicketComments comments = new TicketComments(comment, ticket.Id, user.Id);
            db.TicketComments.Add(comments);
            db.SaveChanges();

            return RedirectToRoute(new { action = "Details", id = ticketId });
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
            string username = User.Identity.Name;
            ApplicationUser currentUser = db.Users.First(u => u.Email == username);
            Tickets ticket = TicketBL.GetTicket((int)ticketId);
            TicketStatuses status = db.TicketStatuses.First(s => s.Name == "Assigned");
            ApplicationUser user = db.Users.Find(userId);
            if (user != null && ticket != null)
            {
                db.Entry(ticket).State = EntityState.Unchanged;
                db.Entry(user).State = EntityState.Unchanged;
                ticket.AssignedToUser = user;
                ticket.AssignedToUserId = user.Id;
                ticket.TicketStatus = status;
                user.AssignedToTickets.Add(ticket);
                TicketHistories history = new TicketHistories("Unassigned", $"Assigned to developer {user.Email}", (int)ticketId, currentUser.Id, Property.AssignedToUser);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            return RedirectToRoute(new { action = "Details", id = ticketId });
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.TicketStatus = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketType = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.TicketPriorities = new SelectList(db.TicketPriorities, "Id", "Name");
            Tickets ticket = TicketBL.GetTicket((int)id);
            ViewBag.Created = ticket.Created;
            ViewBag.OwnerUserId = ticket.OwnerUserId;
            ViewBag.AssignedUserId = ticket.AssignedToUserId;
            ViewBag.ProjectId = ticket.ProjectId;
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Tickets ticket)
        {
            string username = User.Identity.Name;
            ApplicationUser user = db.Users.First(u => u.Email == username);
            Tickets oldTicket = TicketBL.GetTicket((int)id);
            if (ModelState.IsValid)
            {
                TicketBL.UpdateTicket(ticket);
            }
            Tickets newTicket = TicketBL.GetTicket((int)ticket.Id);
            string oldValue;
            string newValue;
            if (oldTicket.Title != ticket.Title)
            {
                oldValue = oldTicket.Title;
                newValue = ticket.Title;
                TicketHistories history = new TicketHistories(oldValue, newValue, ticket.Id, user.Id, Property.Title);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            if (oldTicket.Description != ticket.Description)
            {
                oldValue = oldTicket.Description;
                newValue = ticket.Description;
                TicketHistories history = new TicketHistories(oldValue, newValue, ticket.Id, user.Id, Property.Description);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            if (oldTicket.TicketType.Name != newTicket.TicketType.Name)
            {
                oldValue = oldTicket.TicketType.Name;
                newValue = newTicket.TicketType.Name;
                TicketHistories history = new TicketHistories(oldValue, newValue, ticket.Id, user.Id, Property.Type);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            if (oldTicket.TicketStatus.Name != newTicket.TicketStatus.Name)
            {
                oldValue = oldTicket.TicketStatus.Name;
                newValue = newTicket.TicketStatus.Name;
                TicketHistories history = new TicketHistories(oldValue, newValue, ticket.Id, user.Id, Property.Status);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            if (oldTicket.TicketPriority.Name != newTicket.TicketPriority.Name)
            {
                oldValue = oldTicket.TicketPriority.Name;
                newValue = newTicket.TicketPriority.Name;
                TicketHistories history = new TicketHistories(oldValue, newValue, ticket.Id, user.Id, Property.Priority);
                ticket.TicketHistories.Add(history);
                db.SaveChanges();
            }
            
            return RedirectToRoute(new { action = "Details", id = id });
        }
    }
}
