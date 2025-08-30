using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.UnitOfWorks;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Courses;

public class CourseManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;

    public CourseManager(IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncQueryableExecutor)
    {
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
    }
    
   public async Task<Guid> CreateAsync(string name, string description, Guid teacherId, CancellationToken cancellationToken = default)
    {
        var courseQuery = _unitOfWork.CourseRepo.GetQueryable()
            .Where(c => c.Name == name && c.TeacherId == teacherId);

        var isExist = await _asyncQueryableExecutor.AnyAsync(courseQuery, cancellationToken);
        if (isExist)
            throw new BusinessException("Course already exists.");

        var teacher = await _unitOfWork.UserRepo.GetByIdAsync(teacherId, cancellationToken)
                      ?? throw new BusinessException("Teacher not found");

        if (teacher.Role != UserRole.Teacher)
            throw new BusinessException("User is not a teacher.");

        var course = new Course(name, description, teacher.Id);

        await _unitOfWork.CourseRepo.AddAsync(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return  course.Id;
    }

    public async Task UpdateAsync(Guid courseId, string newName, string newDescription, CancellationToken cancellationToken = default)
    {
        var course = await _unitOfWork.CourseRepo.GetByIdAsync(courseId, cancellationToken)
                          ?? throw new BusinessException("Course not found.");

        course.Update(newName, newDescription);
        _unitOfWork.CourseRepo.Update(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        var courseQuery = _unitOfWork.CourseRepo.GetQueryable()
            .Where(c => c.Id == courseId);

        var course = await _asyncQueryableExecutor.FirstOrDefaultAsync(courseQuery, cancellationToken)
                     ?? throw new BusinessException("Course not found.");

        _unitOfWork.CourseRepo.Remove(course);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}