namespace SchoolManagement.Application.Enrollments.DTOs;

public class EnrollmentRequestDto
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; } 
}