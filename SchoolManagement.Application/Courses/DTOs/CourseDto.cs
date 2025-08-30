namespace SchoolManagement.Application.Courses.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid TeacherId { get; set; }
    public string TeacherName { get; set; }
}