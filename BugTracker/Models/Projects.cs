namespace BugTracker.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectUsers> ProjectUsers { get; set; }
        public ICollection<Tickets> Tickets { get; set; }

        public Projects()
        {
            ProjectUsers = new HashSet<ProjectUsers>();
            Tickets = new HashSet<Tickets>();
        }
    }
}
