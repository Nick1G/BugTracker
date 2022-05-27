namespace BugTracker.Models.AssessmentClasses
{
    public class BugReport : TicketTypes
    {
        public string ErrorCodes { get; set; }
        public string ErrorLogs { get; set; }

        public BugReport()
        {
            DeadlineMultiplier = 2;
        }
    }
}
