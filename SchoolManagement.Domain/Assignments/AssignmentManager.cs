using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.UnitOfWorks;

namespace SchoolManagement.Domain.Assignments;

public class AssignmentManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;

    public AssignmentManager(IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncQueryableExecutor)
    {
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
    }

    public async Task SubmitAssignmentAsync(Guid assignmentId,string studentAnswer, CancellationToken cancellationToken = default)
    {
        var assignment = await _unitOfWork.AssignmentRepo.GetByIdAsync(assignmentId, cancellationToken)
                         ?? throw new BusinessException("Assignment not found");
        assignment.SetStatusSubmit(studentAnswer);
        
        _unitOfWork.AssignmentRepo.Update(assignment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task GradeAssignmentAsync(Guid assignmentId, int gradeValue ,string? feedback , CancellationToken cancellationToken = default)
    {
        var assignment = await _unitOfWork.AssignmentRepo.GetByIdAsync(assignmentId, cancellationToken)
                         ?? throw new BusinessException("Assignment not found");
        
        assignment.SetStatusGrade();
        var grade = new Grade(assignment.Id, gradeValue, DateTime.UtcNow, feedback);
        await _unitOfWork.GradeRepo.AddAsync(grade);
        
        assignment.SetGrade(grade);

        _unitOfWork.AssignmentRepo.Update(assignment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    
    public async Task<Assignment> CreateAssignmentAsync(Guid courseId,string description, DateTime dueDate, Guid studentId , CancellationToken cancellationToken = default)
    {
        var course = await _unitOfWork.CourseRepo.GetByIdAsync(courseId, cancellationToken)
                     ?? throw new BusinessException("Course not found");

        var student = await _unitOfWork.UserRepo.GetByIdAsync(studentId, cancellationToken)
                      ?? throw new BusinessException("Student not found");

       
        var enrollmentExists = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.EnrollmentRepo.GetQueryable()
                .Where(e => e.CourseId == courseId && e.StudentId == studentId),
            cancellationToken);

        if (!enrollmentExists)
            throw new BusinessException("Student is not enrolled in this course");

        var assignment = new Assignment(course.Id, student.Id , dueDate , description);
        
        await _unitOfWork.AssignmentRepo.AddAsync(assignment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return assignment;
    }

}
