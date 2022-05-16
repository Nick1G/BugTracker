using BugTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(u => u.OwnedTickets).WithOne(t => t.OwnerUser).HasForeignKey(u => u.OwnerUserId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ApplicationUser>().HasMany(u => u.AssignedToTickets).WithOne(t => t.AssignedToUser).HasForeignKey(u => u.AssignedToUserId).OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectUsers> ProjectUsers;
        public DbSet<TicketAttachments> TicketAttachments;
        public DbSet<TicketComments> TicketComments;
        public DbSet<TicketHistories> TicketHistories;
        public DbSet<TicketNotifications> TicketNotifications;
        public DbSet<TicketPriorities> TicketPriorities;
        public DbSet<Tickets> Tickets;
        public DbSet<TicketStatuses> TicketStatuses;
        public DbSet<TicketTypes> TicketTypes;
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<BugTracker.Models.Projects>? Projects_1 { get; set; }
    }
}