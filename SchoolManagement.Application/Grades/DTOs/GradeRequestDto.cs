using SchoolManagement.Application.Base;

namespace SchoolManagement.Application.Grades.DTOs;

public class GradeRequestDto : PagingRequestDto
{
    public int? MinValue { get; set; }
    public int? MaxValue { get; set; }

    public Guid? AssignmentId { get; set; }

    public string? SortBy { get; set; } = "Value";
    public bool SortDesc { get; set; } = false;
}