using SchoolManagement.Application.Base;

namespace SchoolManagement.Application.Courses.DTOs;

public class CourseRequestDto : PagingRequestDto
{
    public string? Name { get; set; }
}