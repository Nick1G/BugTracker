using BugTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Models
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //string newRole = new string("Project Manager");
            //await roleManager.CreateAsync(new IdentityRole(newRole));

            //string newRole2 = new string("Developer");
            //await roleManager.CreateAsync(new IdentityRole(newRole2));

            //string newRole3 = new string("Admin");
            //await roleManager.CreateAsync(new IdentityRole(newRole3));

            //string newRole4 = new string("Submitter");
            //await roleManager.CreateAsync(new IdentityRole(newRole4));

            //var passwordHasher = new PasswordHasher<ApplicationUser>();
            //ApplicationUser firstAdmin = new ApplicationUser { Email = "firstAdmin@company.ca", NormalizedEmail = "FIRSTADMIN@COMPANY.CA", UserName = "firstAdmin@company.ca", EmailConfirmed = true };

            //var hashedPassword = passwordHasher.HashPassword(firstAdmin, "P@ssword1");
            //firstAdmin.PasswordHash = hashedPassword;
            //await userManager.CreateAsync(firstAdmin);
            //await userManager.AddToRoleAsync(firstAdmin, "Admin");

            //ApplicationUser secondAdmin = new ApplicationUser { Email = "secondAdmin@company.ca", NormalizedEmail = "SECONDADMIN@COMPANY.CA", UserName = "secondAdmin@company.ca", EmailConfirmed = true };

            //var hashedPassword7 = passwordHasher.HashPassword(secondAdmin, "P@ssword1");
            //secondAdmin.PasswordHash = hashedPassword7;
            //await userManager.CreateAsync(secondAdmin);
            //await userManager.AddToRoleAsync(secondAdmin, "Admin");

            //ApplicationUser user1 = new ApplicationUser { Email = "sam@gmail.com", NormalizedEmail = "SAM@GMAIL.COM", UserName = "sam@gmail.com", EmailConfirmed = true };
            //ApplicationUser user2 = new ApplicationUser { Email = "cat@gmail.com", NormalizedEmail = "CAT@GMAIL.COM", UserName = "cat@gmail.com", EmailConfirmed = true };
            //ApplicationUser user8 = new ApplicationUser { Email = "carly@gmail.com", NormalizedEmail = "CARLY@GMAIL.COM", UserName = "carly@gmail.com", EmailConfirmed = true };
            //ApplicationUser user7 = new ApplicationUser { Email = "gibby@gmail.com", NormalizedEmail = "GIBBY@GMAIL.COM", UserName = "gibby@gmail.com", EmailConfirmed = true };
            //ApplicationUser user3 = new ApplicationUser { Email = "andre@gmail.com", NormalizedEmail = "ANDRE@GMAIL.COM", UserName = "andre@gmail.com", EmailConfirmed = true };
            //ApplicationUser user4 = new ApplicationUser { Email = "tori@gmail.com", NormalizedEmail = "TORI@GMAIL.COM", UserName = "tori@gmail.com", EmailConfirmed = true };
            //ApplicationUser user5 = new ApplicationUser { Email = "manager@gmail.com", NormalizedEmail = "MANAGER@GMAIL.COM", UserName = "manager@gmail.com", EmailConfirmed = true };
            //ApplicationUser user6 = new ApplicationUser { Email = "projectManager@gmail.com", NormalizedEmail = "PPROJECTMANAGER@GMAIL.COM", UserName = "projectManager@gmail.com", EmailConfirmed = true };


            //var hashedPassword2 = passwordHasher.HashPassword(user1, "P@ssword1");
            //user1.PasswordHash = hashedPassword2;
            //await userManager.CreateAsync(user1);
            //await userManager.AddToRoleAsync(user1, "Developer");

            //var hashedPassword3 = passwordHasher.HashPassword(user2, "P@ssword1");
            //user2.PasswordHash = hashedPassword3;
            //await userManager.CreateAsync(user2);
            //await userManager.AddToRoleAsync(user2, "Developer");

            //var hashedPassword9 = passwordHasher.HashPassword(user8, "P@ssword1");
            //user8.PasswordHash = hashedPassword9;
            //await userManager.CreateAsync(user8);
            //await userManager.AddToRoleAsync(user8, "Developer");

            //var hashedPassword10 = passwordHasher.HashPassword(user7, "P@ssword1");
            //user7.PasswordHash = hashedPassword10;
            //await userManager.CreateAsync(user7);
            //await userManager.AddToRoleAsync(user7, "Submitter");

            //var hashedPassword4 = passwordHasher.HashPassword(user3, "P@ssword1");
            //user3.PasswordHash = hashedPassword4;
            //await userManager.CreateAsync(user3);
            //await userManager.AddToRoleAsync(user3, "Submitter");

            //var hashedPassword5 = passwordHasher.HashPassword(user4, "P@ssword1");
            //user4.PasswordHash = hashedPassword5;
            //await userManager.CreateAsync(user4);
            //await userManager.AddToRoleAsync(user4, "Submitter");

            //var hashedPassword6 = passwordHasher.HashPassword(user5, "P@ssword1");
            //user5.PasswordHash = hashedPassword6;
            //await userManager.CreateAsync(user5);
            //await userManager.AddToRoleAsync(user5, "Project Manager");

            //var hashedPassword8 = passwordHasher.HashPassword(user6, "P@ssword1");
            //user6.PasswordHash = hashedPassword8;
            //await userManager.CreateAsync(user6);
            //await userManager.AddToRoleAsync(user6, "Project Manager");

            //TicketTypes type1 = new("Incident");
            //context.TicketTypes.Add(type1);
            //TicketTypes type2 = new("Service Request");
            //context.TicketTypes.Add(type2);
            //TicketTypes type3 = new("Information Request");
            //context.TicketTypes.Add(type3);

            //TicketStatuses status1 = new("Unassigned");
            //context.TicketStatuses.Add(status1);
            //TicketStatuses status2 = new("Assigned");
            //context.TicketStatuses.Add(status2);
            //TicketStatuses status3 = new("In Progress");
            //context.TicketStatuses.Add(status3);
            //TicketStatuses status4 = new("Completed");
            //context.TicketStatuses.Add(status4);

            //TicketPriorities priorities1 = new("High");
            //context.TicketPriorities.Add(priorities1);
            //TicketPriorities priorities2 = new("Medium");
            //context.TicketPriorities.Add(priorities2);
            //TicketPriorities priorities3 = new("Low");
            //context.TicketPriorities.Add(priorities3);

            //context.SaveChanges();
        }
    }
}

