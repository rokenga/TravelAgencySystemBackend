using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgencySystemDomain.Entities;

namespace TravelAgencySystemInfrastructure.Data.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasOne(c => c.Record)
            .WithMany(r => r.comments)
            .HasForeignKey(c => c.RecordId)
            .OnDelete(DeleteBehavior.Cascade); // Enable cascading deletes for Comments when a Record is deleted

        builder
            .HasOne(u => u.Client)
            .WithMany(c => c.comments)
            .HasForeignKey(u => u.ClientID)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes on Client
    }
}
