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

        public DbSet<TicketAttachments> TicketAttachments { get; set; }
        public DbSet<TicketComments> TicketComments { get; set; }
        public DbSet<TicketHistories> TicketHistories { get; set; }
        public DbSet<TicketNotifications> TicketNotifications { get; set; }
        public DbSet<TicketPriorities> TicketPriorities { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketStatuses> TicketStatuses { get; set; }
        public DbSet<TicketTypes> TicketTypes { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}