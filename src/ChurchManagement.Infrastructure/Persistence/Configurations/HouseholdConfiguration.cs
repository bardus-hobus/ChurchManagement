using ChurchManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChurchManagement.Infrastructure.Persistence.Configurations;

public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
{
    public void Configure(EntityTypeBuilder<Household> b)
    {
       {
            b.ToTable("Households");
            b.HasKey(h => h.Id);

            b.Property(h => h.DisplayName)
             .HasMaxLength(200)
             .IsRequired();

            // Address VO
            b.OwnsOne(h => h.Address, ab =>
            {
                ab.Property(a => a.Line1).HasColumnName("Addr_Line1").HasMaxLength(200).IsRequired();
                ab.Property(a => a.Line2).HasColumnName("Addr_Line2").HasMaxLength(200);
                ab.Property(a => a.City).HasColumnName("Addr_City").HasMaxLength(100).IsRequired();
                ab.Property(a => a.State).HasColumnName("Addr_State").HasMaxLength(50).IsRequired();
                ab.Property(a => a.Postal).HasColumnName("Addr_Postal").HasMaxLength(20).IsRequired();
                ab.Property(a => a.Country).HasColumnName("Addr_Country").HasMaxLength(100).IsRequired();
            });

            // Owned collection: HouseholdMember
            b.OwnsMany(h => h.Members, mb =>
            {
                mb.ToTable("HouseholdMembers");

                // Every row belongs to a Household
                mb.WithOwner().HasForeignKey("HouseholdId");

                // Shadow PK for EF (you can switch to composite PK if you prefer)
                mb.Property<int>("Id");
                mb.HasKey("Id");

                mb.Property(m => m.HouseholdId)
                  .HasColumnName("HouseholdId")
                  .IsRequired();

                mb.Property(m => m.MemberId)
                  .HasColumnName("MemberId")
                  .IsRequired();

                mb.Property(m => m.Role)
                  .HasConversion<string>()
                  .HasColumnName("Role")
                  .HasMaxLength(20)
                  .IsRequired();

                mb.Property(m => m.Since)
                  .HasColumnName("Since")
                  .IsRequired();

                mb.Property(m => m.Until)
                  .HasColumnName("Until");

                mb.Property(m => m.Active)
                  .HasColumnName("Active")
                  .IsRequired();

                // Useful indexes for queries
                mb.HasIndex("HouseholdId");
                mb.HasIndex("MemberId", "Active");
            });

            // Field-backed access for the private List<HouseholdMember> _members
            b.Navigation(h => h.Members).UsePropertyAccessMode(PropertyAccessMode.Field);
        } 
    }
}