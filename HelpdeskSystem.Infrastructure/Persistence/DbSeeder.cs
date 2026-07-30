using Microsoft.AspNetCore.Identity;
using HelpdeskSystem.Domain.Entities;
using HelpdeskSystem.Domain.Enums;
using HelpdeskSystem.Infrastructure.Identity;

namespace HelpdeskSystem.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Employee", "Agent", "Admin" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        if (!context.TicketCategories.Any())
        {
            context.TicketCategories.AddRange(
                new TicketCategory { Name = "Şəbəkə" },
                new TicketCategory { Name = "Proqram Təminatı" },
                new TicketCategory { Name = "Avadanlıq" },
                new TicketCategory { Name = "Digər" }
            );
            await context.SaveChangesAsync();
        }

        if (await userManager.FindByEmailAsync("admin@helpdesk.com") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@helpdesk.com",
                Email = "admin@helpdesk.com",
                FullName = "Admin İstifadəçi",
                Role = UserRole.Admin,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // 4. Test Agent istifadəçisi
        if (await userManager.FindByEmailAsync("agent@helpdesk.com") == null)
        {
            var agent = new ApplicationUser
            {
                UserName = "agent@helpdesk.com",
                Email = "agent@helpdesk.com",
                FullName = "IT Agent",
                Role = UserRole.Agent,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(agent, "Agent123!");
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"AGENT YARADILMA XƏTASI: {error.Description}");
                }
            }
            else
            {
                await userManager.AddToRoleAsync(agent, "Agent");
            }
        }

        if (await userManager.FindByEmailAsync("employee@helpdesk.com") == null)
        {
            var employee = new ApplicationUser
            {
                UserName = "employee@helpdesk.com",
                Email = "employee@helpdesk.com",
                FullName = "Test İşçi",
                Role = UserRole.Employee,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(employee, "Employee123!");
            await userManager.AddToRoleAsync(employee, "Employee");
        }
    }
}