using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TownSquareAuth.Data.Migrations;
using TownSquareAuth.Models;

namespace TownSquareAuth.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
}
