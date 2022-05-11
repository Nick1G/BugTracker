﻿using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.DAL
{
    public class ProjectRepository : IRepository<Projects>
    {
        public ApplicationDbContext Context { get; set; }

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
            return Context.Projects.First(p => p.Id == id);
        }

        public Projects Get(Func<Projects, bool> firstFunction)
        {
            return Context.Projects.First(firstFunction);
        }

        public ICollection<Projects> GetAll()
        {
            return Context.Projects.ToList();
        }

        public ICollection<Projects> GetList(Func<Projects, bool> whereFunction)
        {
            return Context.Projects.Where(whereFunction).ToList();
        } 
    }
}
