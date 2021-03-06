namespace BugTracker.Models
{
    public class TicketAttachments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; } = null!;
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string FileUrl { get; set; }

        public TicketAttachments(string filePath, string fileUrl, string description, int ticketId, string userId)
        {
            Created = DateTime.Now;
            FilePath = filePath;
            FileUrl = fileUrl;
            Description = description;
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
