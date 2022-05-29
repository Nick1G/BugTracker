using BugTracker.Data;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BugTracker.DAL
{
    public class ProjectRepository : IRepository<Projects>
    {
        private readonly ApplicationDbContext Context;

        public ProjectRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Add(Projects project)
        {
            Context.Projects.Add(project);
        }

        public void Update(Projects project)
        {
            Context.Projects.Update(project);
        }

        public void Delete(Projects project)
        {
            Context.Projects.Remove(project);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Projects Get(int id)
        {
            return Context.Projects.Include("Users").Include("Tickets").First(p => p.Id == id);
        }

        public Projects Get(Func<Projects, bool> firstFunction)
        {
            return Context.Projects.Include("Tickets").First(firstFunction);
        }

        public IQueryable<Projects> GetAll()
        {
            return Context.Projects.Include("Tickets");
        }

        public IQueryable<Projects> GetList(Expression<Func<Projects, bool>> whereFunction)
        {
            return Context.Projects.Include(p => p.Users).Include("Tickets").Where(whereFunction);
        }
    }
}
