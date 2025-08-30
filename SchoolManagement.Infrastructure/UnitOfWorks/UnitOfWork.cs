using SchoolManagement.Domain;
using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.UnitOfWorks;
using SchoolManagement.Domain.Users;
using SchoolManagement.Infrastructure.Base;

namespace SchoolManagement.Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolManagementDbContext _dbContext;
    
    public IRepository<Course> CourseRepo { get; }
    public IRepository<Enrollment> EnrollmentRepo { get; }
    public IRepository<Grade> GradeRepo { get; }
    public IRepository<Assignment> AssignmentRepo { get; }
    public IRepository<User> UserRepo { get; }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public UnitOfWork(SchoolManagementDbContext dbContext)
    {
        _dbContext = dbContext;
        CourseRepo = new Repository<Course>(_dbContext);
        EnrollmentRepo = new Repository<Enrollment>(_dbContext);
        GradeRepo = new Repository<Grade>(_dbContext);
        AssignmentRepo = new Repository<Assignment>(_dbContext);
        UserRepo = new Repository<User>(_dbContext);
    
    }
    

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}