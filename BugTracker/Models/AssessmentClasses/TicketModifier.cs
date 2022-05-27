namespace BugTracker.Models.AssessmentClasses
{
    public abstract class TicketModifier : TicketTypes
    {
        public abstract int ResponseDeadline();
        public abstract int BreachDeadline();
    }
}
