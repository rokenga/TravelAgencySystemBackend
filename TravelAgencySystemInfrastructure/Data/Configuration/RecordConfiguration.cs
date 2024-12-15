using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemInfrastructure.Data.Configuration;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
    public void Configure(EntityTypeBuilder<Record> builder)
    {
        builder
            .HasOne(r => r.Destination)
            .WithMany(d => d.records)
            .HasForeignKey(r => r.DestinationId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes on Destination

        builder
            .HasOne(u => u.Agent)
            .WithMany(d => d.records)
            .HasForeignKey(u => u.AgentId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes on Agent
    }
}
