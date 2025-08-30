using SchoolManagement.Application.Assignments.DTOs;
using SchoolManagement.Domain.Assignments;

namespace SchoolManagement.Application.Assignments;

public class AssignmentAppService : IAssignmentAppService
{
    private readonly AssignmentManager _assignmentManager;

    public AssignmentAppService(AssignmentManager assignmentManager)
    {
        _assignmentManager = assignmentManager;
    }

    public async Task<Guid> CreateAsync(CreateAssignmentDto dto, CancellationToken cancellationToken = default)
    {
        var assignment = await _assignmentManager.CreateAssignmentAsync(dto.CourseId,dto.Description,dto.DueDate , dto.StudentId, cancellationToken);
        return assignment.Id;
    }

    public async Task SubmitAsync(Guid assignmentId, SubmitAssignmentDto submitAssignmentDto, CancellationToken cancellationToken = default)
    {
        await _assignmentManager.SubmitAssignmentAsync(assignmentId , submitAssignmentDto.StudentAnswer, cancellationToken);
    }

    public async Task GradeAsync(Guid assignmentId ,GradeAssignmentDto dto, CancellationToken cancellationToken = default)
    {
        await _assignmentManager.GradeAssignmentAsync(assignmentId, dto.Value, dto.Feedback, cancellationToken);
    }
}