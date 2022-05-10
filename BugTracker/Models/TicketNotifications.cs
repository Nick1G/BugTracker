namespace BugTracker.Models
{
    public class TicketNotifications
    {
        public int Id { get; set; }
        public string NotificationContent { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TicketNotifications()
        {

        }

        public TicketNotifications(string content, int ticketId, string userId)
        {
            NotificationContent = content;
            TicketId = ticketId;
            UserId = userId;
        }
    }
}
