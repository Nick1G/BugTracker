namespace BugTracker.Models
{
    public class TicketTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tickets> Tickets { get; set; }

        public TicketTypes(string name)
        {
            Name = name;
            Tickets = new HashSet<Tickets>();
        }
    }
}
