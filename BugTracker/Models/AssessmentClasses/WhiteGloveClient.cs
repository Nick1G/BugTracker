namespace BugTracker.Models.AssessmentClasses
{
    public class WhiteGloveClient : TicketModifier
    {
        public TicketTypes TicketType;

        public WhiteGloveClient(TicketTypes type)
        {
            TicketType = type;
        }

        public override int ResponseDeadline()
        {
            return (int)(TicketType.ResponseDeadline() * 0.8);
        }

        public override int BreachDeadline()
        {
            return (int)(TicketType.BreachDeadline() * 0.8);
        }
    }
}
