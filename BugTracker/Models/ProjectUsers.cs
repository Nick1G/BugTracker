namespace BugTracker.Models
{
    public class ProjectUsers
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Projects Project { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ProjectUsers(int projectId, string userId)
        {
            ProjectId = projectId;
            UserId = userId;
        }
    }
}
