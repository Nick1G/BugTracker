using BugTracker.Models.AssessmentClasses;

namespace BugTracker.Models
{
    public abstract class TicketTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DeadlineMultiplier { get; set; }
        public ServiceLevelAgreement SLA { get; set; }

        public ICollection<Tickets> Tickets { get; set; }

        public TicketTypes()
        {
            Tickets = new HashSet<Tickets>();
        }

        public int ResponseDeadline()
        {
            return (int)(SLA.BaseResponseDeadline() * DeadlineMultiplier);
        }

        public int BreachDeadline()
        {
            return (int)(SLA.BaseBreachDeadline() * DeadlineMultiplier);
        }
    }
}
