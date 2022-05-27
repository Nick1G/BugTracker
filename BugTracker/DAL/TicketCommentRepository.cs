using BugTracker.Data;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.DAL
{
    public class TicketCommentRepository : IRepository<TicketComments>
    {
        public ApplicationDbContext Context { get; set; }

        public TicketCommentRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Add(TicketComments ticket)
        {
            Context.TicketComments.Add(ticket);
        }

        public void Update(TicketComments ticket)
        {
            Context.TicketComments.Update(ticket);
        }

        public void Delete(TicketComments ticket)
        {
            Context.TicketComments.Remove(ticket);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TicketComments Get(int id)
        {
            return Context.TicketComments.Include("Ticket").First(t => t.Id == id);
        }

        public TicketComments Get(Func<TicketComments, bool> firstFunction)
        {
            return Context.TicketComments.First(firstFunction);
        }

        public ICollection<TicketComments> GetAll()
        {
            return Context.TicketComments.Include("Project").ToList();
        }

        public ICollection<TicketComments> GetList(Func<TicketComments, bool> whereFunction)
        {
            return Context.TicketComments.Where(whereFunction).ToList();
        }
    }
}
