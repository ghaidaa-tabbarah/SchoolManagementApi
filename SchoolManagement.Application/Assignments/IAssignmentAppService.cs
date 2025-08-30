using SchoolManagement.Application.Assignments.DTOs;

namespace SchoolManagement.Application.Assignments;

public interface IAssignmentAppService
{
    Task<Guid> CreateAsync(CreateAssignmentDto dto, CancellationToken cancellationToken = default);
    Task SubmitAsync(Guid assignmentId,SubmitAssignmentDto submitAssignmentDto, CancellationToken cancellationToken = default);
    Task GradeAsync(Guid assignmentId ,GradeAssignmentDto dto, CancellationToken cancellationToken = default);
}