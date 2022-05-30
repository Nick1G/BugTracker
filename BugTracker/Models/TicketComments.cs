using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    public class TicketComments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TicketComments(string comment, int ticketId, string userId)
        {
            Created = DateTime.Now;
            Comment = comment;
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
