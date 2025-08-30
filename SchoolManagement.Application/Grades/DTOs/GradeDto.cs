using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Grades.DTOs;

public class GradeDto 
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public int Value { get; set; }
    public DateTime GradedAt { get; set; }
    public string? Feedback { get; set; }
}