using Codebymister.Domain.Common;
using Codebymister.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Codebymister.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; } = default!;
    public DbSet<Outreach> Outreaches { get; set; } = default!;
    public DbSet<Conversation> Conversations { get; set; } = default!;
    public DbSet<Proposal> Proposals { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<Maintenance> Maintenances { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserSession> UserSessions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ApplySoftDeleteQueryFilters(modelBuilder);
    }

    public override int SaveChanges()
    {
        ApplyAuditTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditTimestamps()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedAt();
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdatedAt();
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.SoftDelete();
            }
        }
    }

    private static void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            if (!typeof(BaseEntity).IsAssignableFrom(clrType))
                continue;

            var parameter = Expression.Parameter(clrType, "e");

            var deletedAtExpr = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new[] { typeof(DateTime?) },
                parameter,
                Expression.Constant(nameof(BaseEntity.DeletedAt)));

            var notDeletedExpr = Expression.Equal(deletedAtExpr, Expression.Constant(null, typeof(DateTime?)));

            var lambda = Expression.Lambda(notDeletedExpr, parameter);

            modelBuilder.Entity(clrType).HasQueryFilter(lambda);
        }
    }
}
