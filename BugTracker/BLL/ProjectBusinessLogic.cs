using BugTracker.DAL;
using BugTracker.Models;

namespace BugTracker.BLL
{
    public class ProjectBusinessLogic
    {
        IRepository<Projects> repo;

        public ProjectBusinessLogic(IRepository<Projects> repoArg)
        {
            repo = repoArg;
        }
    }
}
