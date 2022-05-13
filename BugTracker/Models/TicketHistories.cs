namespace BugTracker.Models
{
    public class TicketHistories
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; }
        public Property Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Changed { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public TicketHistories()
        {

        }

        public TicketHistories(string oldValue, string newValue, int ticketId, string userId, Property property)
        {
            Changed = DateTime.Now;
            OldValue = oldValue;
            NewValue = newValue;
            TicketId = ticketId;
            UserId = userId;
            Property = property;
        }
    }

    public enum Property
    {
        Title,
        Description,
        Type,
        Status,
        Priority,
        AssignedToUser
    }
}
