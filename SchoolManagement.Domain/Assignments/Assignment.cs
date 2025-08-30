using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Grades;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Assignments;

public class Assignment : AuditEntity
{
    public Guid CourseId { get; private set; }
    public DateTime DueDate { get; private set; }
    public Guid StudentId { get; private set; }

    
    public string Description { get; private set; }
    public string? StudentAnswer { get; private set; }
    public AssignmentStatus Status { get; private set; }

    public Course Course { get; private set; }
    public Grade? Grade { get; private set; }
    public User Student { get; private set; }

    private Assignment()
    {
    }

    public Assignment(Guid courseId, Guid studentId  , DateTime dueDate,string description)
    {
        if (description.Length < 5)
            throw new BusinessException("Description must be at least 5 characters long.");
        CourseId = courseId;
        StudentId = studentId;
        Status = AssignmentStatus.Pending;
        DueDate = dueDate;
        Description = description;
    }


    internal void SetStatusSubmit(string studentAnswer)
    {
        if (Status != AssignmentStatus.Pending)
            throw new BusinessException("Assignment cannot be submitted again or already graded.");

        Status = AssignmentStatus.Submitted;
        StudentAnswer = studentAnswer;
        
    }

    internal void SetStatusGrade()
    {
        if (Status != AssignmentStatus.Submitted)
            throw new BusinessException("Assignment must be submitted before grading.");

        Status = AssignmentStatus.Graded;
    }

    internal void SetGrade(Grade grade)
    {
        Grade = grade;
    }
}