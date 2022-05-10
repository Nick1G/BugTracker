using Microsoft.AspNetCore.Identity;

namespace BugTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ProjectUsers> ProjectUsers { get; set; }
        public ICollection<TicketAttachments> TicketAttachments { get; set; }
        public ICollection<TicketComments> TicketComments { get; set; }
        public ICollection<TicketHistories> TicketHistories { get; set; }
        public ICollection<Tickets> OwnedTickets { get; set; }
        public ICollection<Tickets> AssignedToTickets { get; set; }

        public ApplicationUser()
        {
            ProjectUsers = new HashSet<ProjectUsers>();
            TicketAttachments = new HashSet<TicketAttachments>();
            TicketComments = new HashSet<TicketComments>();
            TicketHistories = new HashSet<TicketHistories>();
            OwnedTickets = new HashSet<Tickets>();
            AssignedToTickets = new HashSet<Tickets>();
        }
    }
}
