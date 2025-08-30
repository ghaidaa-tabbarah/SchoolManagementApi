using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.UnitOfWorks;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Enrollments;

public class EnrollmentManger
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;

    public EnrollmentManger(IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncQueryableExecutor)
    {
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
    }

    public async Task CreateAsync(Guid courseId, Guid studentId, CancellationToken cancellationToken = default)
    {
        var enrollmentsQuery = _unitOfWork.EnrollmentRepo.GetQueryable()
            .Where(enrollment =>
                enrollment.CourseId == courseId
                && enrollment.StudentId == studentId
            );
        
        var isExist = await _asyncQueryableExecutor.AnyAsync(enrollmentsQuery, cancellationToken);

        if (isExist)
            throw new BusinessException("Enrollment already exist");

        var course = await _unitOfWork.CourseRepo.GetByIdAsync(courseId, cancellationToken)
            ?? throw new BusinessException("Course not found");
        var student = await _unitOfWork.UserRepo.GetByIdAsync(studentId, cancellationToken)
                      ?? throw new BusinessException("student not found");
        
        if (student.Role != UserRole.Student)
            throw new BusinessException("Student role doesn't match");

        var enrollment = new Enrollment(student.Id, course.Id);
         await _unitOfWork.EnrollmentRepo.AddAsync(enrollment);
         await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}