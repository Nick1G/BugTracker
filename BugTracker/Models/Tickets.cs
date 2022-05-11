namespace BugTracker.Models
{
    public class Tickets
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } = null;
        public int ProjectId { get; set; }
        public Projects Project { get; set; }
        public int TicketTypeId { get; set; }
        public TicketTypes TicketType { get; set; }
        public int TicketPriorityId { get; set; }
        public TicketPriorities TicketPriority { get; set; }
        public int TicketStatusId { get; set; }
        public TicketStatuses TicketStatus { get; set; }
        public string OwnerUserId { get; set; }
        public ApplicationUser OwnerUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public ApplicationUser? AssignedToUser { get; set; }

        public ICollection<TicketAttachments> TicketAttachments { get; set; }
        public ICollection<TicketComments> TicketComments { get; set; }
        public ICollection<TicketHistories> TicketHistories { get; set; }
        public ICollection<TicketNotifications> TicketNotifications { get; set; }

        public Tickets()
        {
            TicketAttachments = new HashSet<TicketAttachments>();
            TicketComments = new HashSet<TicketComments>();
            TicketHistories = new HashSet<TicketHistories>();
            TicketNotifications = new HashSet<TicketNotifications>();
        }

        public Tickets(string title, string desc, int projectId, int typeId, int statusId, string ownerUserId, string assignedUserId)
        {
            Created = DateTime.Now;
            Title = title;
            Description = desc;
            ProjectId = projectId;
            TicketTypeId = typeId;
            TicketStatusId = statusId;
            OwnerUserId = ownerUserId;
            AssignedToUserId = assignedUserId;

            TicketAttachments = new HashSet<TicketAttachments>();
            TicketComments = new HashSet<TicketComments>();
            TicketHistories = new HashSet<TicketHistories>();
            TicketNotifications = new HashSet<TicketNotifications>();
        }
    }
}
