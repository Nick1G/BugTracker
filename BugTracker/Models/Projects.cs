namespace BugTracker.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Tickets> Tickets { get; set; }

        public Projects()
        {
            Users = new HashSet<ApplicationUser>();
            Tickets = new HashSet<Tickets>();
        }
    }
}
