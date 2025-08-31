using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Courses;
using SchoolManagement.Application.Courses.DTOs;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseAppService _courseAppService;
    private readonly ICurrentUser _currentUser;

    public CourseController(ICourseAppService courseAppService, ICurrentUser currentUser)
    {
        _courseAppService = courseAppService;
        _currentUser = currentUser;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCourseDto input, CancellationToken cancellationToken)
    {
        var courseId = await _courseAppService.CreateAsync(input, cancellationToken);
        return Ok(new { CourseId = courseId });
    }
    
    [HttpPost("create-by-teacher")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateByTeacher([FromBody] CreateCourseTeacherDto input, CancellationToken cancellationToken)
    {
        var courseId = await _courseAppService.CreateByTeacherAsync(input, cancellationToken);
        return Ok(new { CourseId = courseId });
    }


    [HttpPut("{id}/update")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCourseDto input,
        CancellationToken cancellationToken)
    {
        await _courseAppService.UpdateAsync(id, input, cancellationToken);
        return Ok(new { Message = "Course updated successfully" });
    }

    [HttpDelete("{id}/delete")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _courseAppService.DeleteAsync(id, cancellationToken);
        return Ok(new { Message = "Course deleted successfully" });
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> GetAll([FromQuery] CourseRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var result = await _courseAppService.GetAllAsync(requestDto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("teacher")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> GetByTeacher(
        [FromQuery] CourseRequestDto requestDto, CancellationToken cancellationToken)
    {
        var teacherId = _currentUser.UserId ?? throw new BusinessException("User not found");
        var result = await _courseAppService.GetByTeacherAsync(teacherId, requestDto, cancellationToken);
        return Ok(result);
    }

    [HttpGet("student")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<IActionResult> GetByStudent( [FromQuery] CourseRequestDto requestDto, CancellationToken cancellationToken)
    {
        var studentId = _currentUser.UserId ?? throw new BusinessException("User not found");
        var result = await _courseAppService.GetByStudentAsync(studentId, requestDto, cancellationToken);
        return Ok(result);
    }
}