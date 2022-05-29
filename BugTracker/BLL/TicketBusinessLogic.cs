using BugTracker.DAL;
using BugTracker.Models;
using System.Linq.Expressions;

namespace BugTracker.BLL
{
    public class TicketBusinessLogic
    {
        IRepository<Tickets> repo;
        public TicketBusinessLogic(IRepository<Tickets> repoArg)
        {
            repo = repoArg;
        }

        public IQueryable<Tickets> AllTickets()
        {
            return repo.GetAll();
        }

        public IQueryable<Tickets> GetTicketsList(Expression<Func<Tickets, bool>> whereFunc)
        {
            return repo.GetList(whereFunc);
        }

        public IQueryable<Tickets> GetAssignedTickets(ApplicationUser user)
        {
            return GetTicketsList(t => t.AssignedToUserId == user.Id);
        }

        public IQueryable<Tickets> GetOwnedTickets(ApplicationUser user)
        {
            return GetTicketsList(t => t.OwnerUserId == user.Id);
        }

        public Tickets GetTicket(int id)
        {
            return repo.Get(id);
        }

        public void CreateTicket(Tickets ticket)
        {
            repo.Add(ticket);
            repo.Save();
        }

        public void UpdateTicket(Tickets ticket)
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
