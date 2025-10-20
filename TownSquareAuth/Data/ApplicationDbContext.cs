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
    public DbSet<EventRSVP> EventRSVPs { get; set; }  = null!;
}
