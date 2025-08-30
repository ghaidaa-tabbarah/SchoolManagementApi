using SchoolManagement.Application.Enrollments.DTOs;

namespace SchoolManagement.Application.Enrollments;

public interface IEnrollmentAppService
{
    Task EnrollStudentAsync(EnrollmentRequestDto requestDto, CancellationToken cancellationToken = default);
}