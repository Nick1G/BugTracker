using BugTracker.DAL;
using BugTracker.Models;

namespace BugTracker.BLL
{
    public class TicketBusinessLogic
    {
        IRepository<Tickets> repo;

        public TicketBusinessLogic(IRepository<Tickets> repoArg)
        {
            repo = repoArg;
        }
    }
}
