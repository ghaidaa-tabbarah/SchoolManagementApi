using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Enrollments;

public class Enrollment : AuditEntity
{
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }

    public Course Course { get; private set; }
    public User Student { get; private set; }

    private Enrollment()
    {
    }

    internal Enrollment(Guid studentId, Guid courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = DateTime.UtcNow;
    }
}