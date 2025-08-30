using SchoolManagement.Application.Enrollments.DTOs;
using SchoolManagement.Domain.Enrollments;

namespace SchoolManagement.Application.Enrollments;

public class EnrollmentAppService : IEnrollmentAppService
{
    private readonly EnrollmentManger _enrollmentManager;

    public EnrollmentAppService(EnrollmentManger enrollmentManager)
    {
        _enrollmentManager = enrollmentManager;
    }

    public async Task EnrollStudentAsync(EnrollmentRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        await _enrollmentManager.CreateAsync(requestDto.CourseId, requestDto.StudentId, cancellationToken);
    }
}