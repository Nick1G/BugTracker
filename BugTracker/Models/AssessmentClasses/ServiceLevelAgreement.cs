namespace BugTracker.Models.AssessmentClasses
{
    public class ServiceLevelAgreement
    {
        public int ResponseDeadline { get; set; }
        public int BreachDeadline { get; set; }

        public int BaseResponseDeadline()
        {
            return ResponseDeadline;
        }

        public int BaseBreachDeadline()
        {
            return BreachDeadline;
        }
    }
}