using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Domain.Grades;

public class Grade : AuditEntity
{

    public Guid AssignmentId { get; private set; }
    
    public int Value { get; private set; }

    public DateTime GradedAt { get; private set; }

    public string? Feedback { get; private set; }

    public Assignment Assignment { get; private set; }

    private Grade()
    {
    }
    
    internal Grade(Guid assignmentId, int value, DateTime gradedAt, string? feedback)
    {
        if (value < 0)
            throw new BusinessException("Grade value cannot be negative.");
        
        if (gradedAt > DateTime.UtcNow)
            throw new BusinessException("GradedAt cannot be in the future.");

        if (!string.IsNullOrWhiteSpace(feedback) && feedback.Length > 500)
            throw new BusinessException("Feedback is too long.");
        AssignmentId = assignmentId;
        Value = value;
        GradedAt = gradedAt;
        Feedback = feedback;
    }

}