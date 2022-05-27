using BugTracker.Data;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.DAL
{
    public class TicketRepository : IRepository<Tickets>
    {
        public ApplicationDbContext Context { get; set; }

        public TicketRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Add(Tickets ticket)
        {
            Context.Tickets.Add(ticket);
        }

        public void Update(Tickets ticket)
        {
            Context.Tickets.Update(ticket);
        }

        public void Delete(Tickets ticket)
        {
            Context.Tickets.Remove(ticket);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Tickets Get(int id)
        {
            return Context.Tickets.AsNoTracking().Include("Project")
                                  .Include("TicketType")
                                  .Include("TicketStatus")
                                  .Include("TicketPriority")
                                  .Include("OwnerUser")
                                  .Include("AssignedToUser")
                                  .Include("TicketHistories")
                                  .Include("TicketComments")
                                  .Include("TicketComments.User")
                                  .Include("TicketHistories.User").First(t => t.Id == id);
        }

        public Tickets Get(Func<Tickets, bool> firstFunction)
        {
            return Context.Tickets.Include("Project")
                                  .Include("TicketType")
                                  .Include("TicketStatus")
                                  .Include("TicketPriority")
                                  .Include("OwnerUser")
                                  .Include("AssignedToUser")
                                  .Include("TicketComments")
                                  .Include("TicketHistories").First(firstFunction);
        }

        public ICollection<Tickets> GetAll()
        {
            return Context.Tickets.Include("Project")
                                  .Include("TicketType")
                                  .Include("TicketStatus")
                                  .Include("TicketPriority")
                                  .Include("OwnerUser")
                                  .Include("AssignedToUser")
                                  .Include("TicketHistories").ToList();
        }

        public ICollection<Tickets> GetList(Func<Tickets, bool> whereFunction)
        {
            return Context.Tickets.Include("Project")
                                  .Include("TicketType")
                                  .Include("TicketStatus")
                                  .Include("TicketPriority")
                                  .Include("OwnerUser")
                                  .Include("AssignedToUser")
                                  .Include("TicketComments")
                                  .Include("TicketHistories").Where(whereFunction).ToList();
        }
    }
}
