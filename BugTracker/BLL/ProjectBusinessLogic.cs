using BugTracker.DAL;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace BugTracker.BLL
{
    public class ProjectBusinessLogic
    {
        IRepository<Projects> repo;

        public ProjectBusinessLogic(IRepository<Projects> repoArg)
        {
            repo = repoArg;
        }

        public IQueryable<Projects> AllProjects()
        {
            return repo.GetAll();
        }

        public IQueryable<Projects> GetProjectsList(Expression<Func<Projects, bool>> whereFunc)
        {
            return repo.GetList(whereFunc);
        }

        public IQueryable<Projects> GetAssignedProjects(ApplicationUser user)
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