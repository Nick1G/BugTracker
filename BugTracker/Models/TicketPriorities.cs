namespace BugTracker.Models
{
    public class TicketPriorities
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tickets> Tickets { get; set; }

        public TicketPriorities(string name)
        {
            Name = name;
            Tickets = new HashSet<Tickets>();
        }
     }
}
