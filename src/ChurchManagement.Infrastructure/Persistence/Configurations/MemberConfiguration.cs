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
            name.Property(p => p.First).HasColumnName("FirstName")
                .IsRequired().HasMaxLength(100);
            name.Property(p => p.Last).HasColumnName("LastName")
                .IsRequired().HasMaxLength(100);

            name.WithOwner();
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

        // ---------------------------
        // Dates & Enums
        // ---------------------------
        // If you’re on EF Core 8 + SQL Server 2022, DateOnly works natively.
        // If you need broader support, uncomment the converters below.
        // var dateOnlyToDate = new ValueConverter<DateOnly, DateTime>(
        //     d => d.ToDateTime(TimeOnly.MinValue),
        //     dt => DateOnly.FromDateTime(DateTime.SpecifyKind(dt, DateTimeKind.Unspecified)));
        //
        // e.Property(x => x.BirthDate).HasConversion(dateOnlyToDate).IsRequired();
        // e.Property(x => x.MembershipDate).HasConversion(dateOnlyToDate).IsRequired();
        // e.Property(x => x.BaptizedDate).HasConversion(dateOnlyToDate);

        e.Property(x => x.BirthDate).IsRequired();
        e.Property(x => x.MembershipDate).IsRequired();
        e.Property(x => x.BaptizedDate);

        e.Property(x => x.Gender)
         .HasConversion<string>()       // store as nvarchar; use int if you prefer
         .HasMaxLength(16)
         .IsRequired();

        // ---------------------------
        // Spouse (optional 1-1 self ref)
        // ---------------------------
        // Shadow FK column "SpouseId" on Members
        e.HasOne(x => x.Spouse)
         .WithOne()
         .HasForeignKey<Member>("SpouseId")
         .OnDelete(DeleteBehavior.NoAction); // avoid cascade cycles
        e.Property<Guid?>("SpouseId");

        // ---------------------------
        // Children (self many-to-many)
        // ---------------------------
        // Your domain exposes IReadOnlyList<Member> Children with a backing field _children.
        // Map it as a many-to-many via a join table.
        e.Navigation(nameof(Member.Children))
         .UsePropertyAccessMode(PropertyAccessMode.Field);

        e.HasMany(x => x.Children)
         .WithMany()
         .UsingEntity<Dictionary<string, object>>(
            "MemberChildren",
            j => j.HasOne<Member>().WithMany().HasForeignKey("ChildId")
                  .OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<Member>().WithMany().HasForeignKey("ParentId")
                  .OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.ToTable("MemberChildren");
                j.HasKey("ParentId", "ChildId");
                j.HasIndex("ChildId");
            });

        // ---------------------------
        // Private collections: _groups (List<string>), _permissions (List<PermissionType>)
        // Stored as JSON in columns GroupsJson / PermissionsJson
        // ---------------------------
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        var stringListToJson = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, jsonOptions),
            v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>());

        var permissionListToJson = new ValueConverter<List<PermissionType>, string>(
            v => JsonSerializer.Serialize(v, jsonOptions),
            v => JsonSerializer.Deserialize<List<PermissionType>>(v, jsonOptions) ?? new List<PermissionType>());

        // Backing fields
        e.Metadata.FindNavigation(nameof(Member.Groups))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        e.Metadata.FindNavigation(nameof(Member.Permissions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        // Map field-only lists to shadow properties via backing fields pattern:
        // We expose them using read-only projections through computed properties below.
        e.Property<List<string>>("_groups")
            .HasColumnName("GroupsJson")
            .HasConversion(stringListToJson);

        e.Property<List<PermissionType>>("_permissions")
            .HasColumnName("PermissionsJson")
            .HasConversion(permissionListToJson);

        // ---------------------------
        // Indexes (examples)
        // ---------------------------
        // If you often query by last name:
        e.HasIndex("LastName"); // from Name owned type column

        // If email must be unique across members, move to true above:
        // e.OwnsOne(x => x.Email, ... unique true) // or create unique index on Members(Email)

        // ---------------------------
        // Misc
        // ---------------------------
        // If your base Entity defines CreatedAt/UpdatedAt, configure here:
        // e.Property<DateTime>("CreatedAt").IsRequired();
        // e.Property<DateTime>("UpdatedAt").IsRequired();
    }
}
