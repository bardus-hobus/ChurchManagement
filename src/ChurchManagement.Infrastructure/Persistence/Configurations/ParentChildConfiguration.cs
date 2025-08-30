using ChurchManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChurchManagement.Infrastructure.Persistence.Configurations;

public class ParentChildConfiguration : IEntityTypeConfiguration<ParentChild>
{
    public void Configure(EntityTypeBuilder<ParentChild> b)
    {
        b.ToTable("ParentChildLinks");
        b.HasKey(pc => pc.Id);

        b.Property(pc => pc.ParentId).IsRequired();
        b.Property(pc => pc.ChildId).IsRequired();

        b.Property(pc => pc.Type)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        b.Property(pc => pc.Since).IsRequired();
        b.Property(pc => pc.Until);
        b.Property(pc => pc.Active).IsRequired();

        // Optional explicit FKs back to Members (safe delete behavior)
        b.HasOne<Member>()
            .WithMany()
            .HasForeignKey(pc => pc.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasOne<Member>()
            .WithMany()
            .HasForeignKey(pc => pc.ChildId)
            .OnDelete(DeleteBehavior.Restrict);

        // Helpful indexes
        b.HasIndex(pc => new { pc.ParentId, pc.Active });
        b.HasIndex(pc => new { pc.ChildId, pc.Active });
    }
}