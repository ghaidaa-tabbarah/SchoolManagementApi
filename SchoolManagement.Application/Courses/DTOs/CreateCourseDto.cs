namespace SchoolManagement.Application.Courses.DTOs;

public class CreateCourseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid TeacherId { get; set; }
}