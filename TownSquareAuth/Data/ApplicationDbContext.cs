using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Models;

namespace TownSquareAuth.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventRSVP> EventRSVPs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) // Configure cascade delete for events when a user is deleted
    {
        base.OnModelCreating(builder);
        builder.Entity<Event>()
            .HasOne(e => e.ApplicationUser)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.ApplicationUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
