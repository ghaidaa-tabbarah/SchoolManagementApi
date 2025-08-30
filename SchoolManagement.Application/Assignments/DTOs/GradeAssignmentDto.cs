using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Assignments.DTOs;

public class GradeAssignmentDto
{
    [Range(0, int.MaxValue)]
    public int Value { get; set; }
    public string? Feedback { get; set; }
}