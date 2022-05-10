namespace BugTracker.Models
{
    public class TicketStatuses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tickets> Tickets { get; set; }

        public TicketStatuses(string name)
        {
            Name = name;
            Tickets = new HashSet<Tickets>();
        }
    }
}
