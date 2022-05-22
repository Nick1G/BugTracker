using Microsoft.AspNetCore.Mvc;
using BugTracker.BLL;
using BugTracker.DAL;
using BugTracker.Data;
using Microsoft.AspNetCore.Authorization;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        private TicketBusinessLogic TicketBL { get; set; }

        public TicketController(ApplicationDbContext context)
        {
            TicketBL = new TicketBusinessLogic(new TicketRepository(context));
        }

        public IActionResult Index()
        {
            return View(TicketBL.AllTickets());
        }

        [Authorize(Roles = "Submitter")]
        public IActionResult Create(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
