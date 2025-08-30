using SchoolManagement.Application.Base;
using SchoolManagement.Application.Grades.DTOs;

namespace SchoolManagement.Application.Grades;

public interface IGradeAppService
{
    Task<PagedResponseDto<GradeDto>> GetAllAsync(GradeRequestDto requestDto, CancellationToken cancellationToken = default);
}