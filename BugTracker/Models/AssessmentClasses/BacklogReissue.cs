namespace BugTracker.Models.AssessmentClasses
{
    public class BacklogReissue : TicketModifier
    {
        public TicketTypes TicketType;

        public BacklogReissue(TicketTypes type)
        {
            TicketType = type;
        }

        public override int ResponseDeadline()
        {
            return (int)(TicketType.ResponseDeadline() + 100);
        }

        public override int BreachDeadline()
        {
            return (int)(TicketType.BreachDeadline() + 100);
        }
    }
}
