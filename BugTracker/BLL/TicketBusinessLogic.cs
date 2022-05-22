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

        public List<Tickets> AllTickets()
        {
            return repo.GetAll().ToList();
        }

        public List<Tickets> GetTicketsList(Func<Tickets, bool> whereFunc)
        {
            return repo.GetList(whereFunc).ToList();
        }

        public List<Tickets> GetAssignedTickets(ApplicationUser user)
        {
            return GetTicketsList(p => p.AssignedToUserId == user.Id);
        }

        public Tickets GetTicket(int id)
        {
            return repo.Get(id);
        }

        public void CreateProject(Tickets ticket)
        {
            repo.Add(ticket);
            repo.Save();
        }

        public void UpdateProject(Tickets ticket)
        {
            repo.Update(ticket);
            repo.Save();
        }

        public void DeleteTicket(Tickets ticket)
        {
            repo.Delete(ticket);
            repo.Save();
        }
    }
}
