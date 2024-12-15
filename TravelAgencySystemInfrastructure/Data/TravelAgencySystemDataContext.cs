using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgencySystemDomain.Entities;
using TravelAgencySystemInfrastructure.Data.Configuration;

namespace TravelAgencySystemInfrastructure.Data;

public class TravelAgencySystemDataContext : IdentityDbContext<User>
{
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public TravelAgencySystemDataContext(DbContextOptions<TravelAgencySystemDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DestinationConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecordConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly);
    }
}