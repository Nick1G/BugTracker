using BugTracker.DAL;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.BLL
{
    public class ProjectBusinessLogic
    {
        IRepository<Projects> repo;

        public ProjectBusinessLogic(IRepository<Projects> repoArg)
        {
            repo = repoArg;
        }

        public List<Projects> AllProjects()
        {
            return repo.GetAll().ToList();
        }

        public List<Projects> GetProjectsList(Func<Projects, bool> whereFunc)
        {
            return repo.GetList(whereFunc).ToList();
        }

        public List<Projects> GetAssignedProjects(ApplicationUser user)
        {
            return GetProjectsList(p => p.Users.Any(s => s.Id == user.Id));
        }

        public Projects GetProject(int id)
        {
            return repo.Get(id);
        }

        public void CreateProject(Projects project)
        {
            repo.Add(project);
            repo.Save();
        }

        public void UpdateProject(Projects project)
        {
            repo.Update(project);
            repo.Save();
        }

        public void DeleteProject(Projects project)
        {
            repo.Delete(project);
            repo.Save();
        }
    }
}
