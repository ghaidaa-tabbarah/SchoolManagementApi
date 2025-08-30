using SchoolManagement.Application.Base;
using SchoolManagement.Application.Courses.DTOs;

namespace SchoolManagement.Application.Courses;

public interface ICourseAppService
{
    Task<Guid> CreateAsync(CreateCourseDto input, CancellationToken cancellationToken = default);
    Task<Guid> CreateByTeacherAsync(CreateCourseTeacherDto input, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateCourseDto input, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid courseId, CancellationToken cancellationToken = default);

    Task<PagedResponseDto<CourseDto>> GetAllAsync(CourseRequestDto requestDto,
        CancellationToken cancellationToken = default);

    Task<PagedResponseDto<CourseDto>> GetByTeacherAsync(Guid teacherId, CourseRequestDto requestDto,
        CancellationToken cancellationToken = default);

    Task<PagedResponseDto<CourseDto>> GetByStudentAsync(Guid studentId, CourseRequestDto requestDto, CancellationToken cancellationToken = default);
}