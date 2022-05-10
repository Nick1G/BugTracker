namespace BugTracker.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string FileUrl { get; set; }

        public TicketAttachments(string description, int ticketId, string userId)
        {
            Created = DateTime.Now;
            Description = description;
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
