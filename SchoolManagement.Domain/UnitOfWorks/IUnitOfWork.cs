using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    public IRepository<Course> CourseRepo { get; }
    public IRepository<Enrollment> EnrollmentRepo { get; }
    public IRepository<Grade> GradeRepo { get; }
    public IRepository<Assignment> AssignmentRepo { get; }
    public IRepository<User> UserRepo { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}