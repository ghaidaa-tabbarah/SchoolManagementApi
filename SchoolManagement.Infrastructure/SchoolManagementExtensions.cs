using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Infrastructure;

public static class SchoolManagementExtensions
{
    public static void ConfigureSchoolManagementEntities(this ModelBuilder builder)
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));

        builder.Entity<User>(entity =>
        {
            entity.OwnsOne(user => user.Name);
        });

        builder.Entity<Course>(entity =>
        {
            entity.HasOne(c => c.Teacher)
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Enrollment>(entity =>
        {
            entity.HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Assignment>(entity =>
        {
            entity.Property(a => a.Description).IsRequired().HasMaxLength(1000);
            entity.Property(a => a.StudentAnswer).HasMaxLength(1000);

            entity.HasOne(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Grade)
                .WithOne(g => g.Assignment)
                .HasForeignKey<Grade>(g => g.AssignmentId);
        });

        builder.Entity<Grade>(entity =>
        {
            
        });
    }
}
