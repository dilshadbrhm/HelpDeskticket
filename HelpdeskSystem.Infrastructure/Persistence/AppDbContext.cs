using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Domain.Entities;
using HelpdeskSystem.Infrastructure.Identity;

namespace HelpdeskSystem.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketCategory> TicketCategories { get; set; }
    public DbSet<TicketComment> TicketComments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tickets)
            .HasForeignKey(t => t.CategoryId);

        builder.Entity<TicketComment>()
            .HasOne(c => c.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TicketId);
    }
}