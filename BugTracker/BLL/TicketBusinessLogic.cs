﻿using BugTracker.DAL;
using BugTracker.Models;

namespace BugTracker.BLL
{
    public class TicketBusinessLogic
    {
        IRepository<Tickets> repo;
        IRepository<TicketComments> ticketCommentRepo;

        public TicketBusinessLogic(IRepository<Tickets> repoArg, IRepository<TicketComments> ticketCommentRepo)
        {
            repo = repoArg;
            this.ticketCommentRepo = ticketCommentRepo;
        }

        public List<Tickets> AllTickets()
        {
            return repo.GetAll().ToList();
        }

        public List<Tickets> GetTicketsList(Func<Tickets, bool> whereFunc)
        {
            return repo.GetList(whereFunc).ToList();
        }

        public List<Tickets> GetAssignedTickets(ApplicationUser user)
        {
            return GetTicketsList(t => t.AssignedToUserId == user.Id);
        }

        public List<Tickets> GetOwnedTickets(ApplicationUser user)
        {
            return GetTicketsList(t => t.OwnerUserId == user.Id);
        }

        public Tickets GetTicket(int id)
        {
            return repo.Get(id);
        }

        public void CreateTicket(Tickets ticket)
        {
            repo.Add(ticket);
            repo.Save();
        }

        public void UpdateTicket(Tickets ticket)
        {
            repo.Update(ticket);
            repo.Save();
        }

        public void DeleteTicket(Tickets ticket)
        {
            repo.Delete(ticket);
            repo.Save();
        }

        public void Comment(TicketComments entity)
        {
            ticketCommentRepo.Add(entity);
            ticketCommentRepo.Save();
        }
    }
}
