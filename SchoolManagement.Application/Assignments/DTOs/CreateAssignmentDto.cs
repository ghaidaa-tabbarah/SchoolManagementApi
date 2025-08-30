namespace SchoolManagement.Application.Assignments.DTOs;

public class CreateAssignmentDto
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}