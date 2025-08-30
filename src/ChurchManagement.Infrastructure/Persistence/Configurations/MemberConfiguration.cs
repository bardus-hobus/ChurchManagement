using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ChurchManagement.Domain.Entities;
using ChurchManagement.Domain.Enums;
using ChurchManagement.Domain.ValueObjects;

namespace ChurchManagement.Infrastructure.Persistence.Configurations;

public sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> e)
    {
        e.ToTable("Members");
        e.HasKey(x => x.Id);

        e.OwnsOne(x => x.Name, name =>
        {
            name.Property(p => p.FirstName).HasColumnName("FirstName")
                .IsRequired().HasMaxLength(100);
            name.Property(p => p.LastName).HasColumnName("LastName")
                .IsRequired().HasMaxLength(100);

        });

        // Email (nullable)
        e.OwnsOne(x => x.Email, email =>
        {
            email.Property(p => p.Value)
                 .HasColumnName("Email")
                 .HasMaxLength(320);
            email.HasIndex(p => p.Value).IsUnique(false); // flip to true if you want global uniqueness
        });

        // PhoneNumber (nullable)
        e.OwnsOne(x => x.PhoneNumber, phone =>
        {
            phone.Property(p => p.Value)
                 .HasColumnName("PhoneNumber")
                 .HasMaxLength(32);
        });

        e.Property(x => x.BirthDate).IsRequired();
        e.Property(x => x.MembershipDate).IsRequired();
        e.Property(x => x.BaptizedDate);
        e.Property(x => x.CreatedAt).IsRequired();
        e.Property(x => x.UpdatedAt).IsRequired();

        e.Property(x => x.Gender)
         .HasConversion<string>()       // store as nvarchar; use int if you prefer
         .HasMaxLength(16)
         .IsRequired();
        
        e.HasIndex("LastName"); // from Name owned type column

    }
}
