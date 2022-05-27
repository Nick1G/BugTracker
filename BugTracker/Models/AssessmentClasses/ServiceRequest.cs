namespace BugTracker.Models.AssessmentClasses
{
    public class ServiceRequest : TicketTypes
    {
        public Requests Request { get; set; }

        public ServiceRequest()
        {
            DeadlineMultiplier = 1.5;
        }
    }

    public enum Requests
    {
        SomeRequest,
        OtherRequest,
        CoolRequest,
    }
}
