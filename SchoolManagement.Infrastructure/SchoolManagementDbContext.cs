using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Infrastructure;

public class SchoolManagementDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;
    
    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options, ICurrentUser currentUser) :
        base(options)
    {
        _currentUser = currentUser;
    }

    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options) :
        base(options)
    {
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Grade> Grades { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigureSchoolManagementEntities();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditEntity)
            .ToList();
        UpdateSoftDelete(entities);
        UpdateTimestamps(entities);
    }

    private static void UpdateSoftDelete(IEnumerable<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State is EntityState.Added or EntityState.Deleted);

        foreach (var entry in filtered)
            switch (entry.State)
            {
                case EntityState.Added:
                    ((AuditEntity)entry.Entity).IsDeleted = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    ((AuditEntity)entry.Entity).IsDeleted = true;
                    break;
            }
    }

    private void UpdateTimestamps(IEnumerable<EntityEntry> entries)
    {
        var filtered = entries
            .Where(x => x.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in filtered)
        {
            if (entry.State == EntityState.Added)
            {
                ((AuditEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                ((AuditEntity)entry.Entity).CreatedBy = _currentUser?.UserId;
            }

            ((AuditEntity)entry.Entity).UpdatedAt = DateTime.UtcNow;
            ((AuditEntity)entry.Entity).UpdatedBy = _currentUser?.UserId;
        }
    }
}
