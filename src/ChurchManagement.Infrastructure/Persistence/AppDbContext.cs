// Infrastructure/Persistence/AppDbContext.cs

using ChurchManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChurchManagement.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public DbSet<Member> Members => Set<Member>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(b);
    }
}