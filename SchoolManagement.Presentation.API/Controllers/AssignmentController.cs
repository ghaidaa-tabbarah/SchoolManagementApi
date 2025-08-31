using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Assignments;
using SchoolManagement.Application.Assignments.DTOs;

namespace SchoolManagement.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentAppService _assignmentAppService;

        public AssignmentController(IAssignmentAppService assignmentAppService)
        {
            _assignmentAppService = assignmentAppService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateAssignmentDto dto, CancellationToken cancellationToken)
        {
            var assignmentId = await _assignmentAppService.CreateAsync(dto, cancellationToken);
            return Ok(new { AssignmentId = assignmentId });
        }


        [HttpPost("{assignmentId}/submit")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Submit([FromRoute] Guid assignmentId,
            [FromBody] SubmitAssignmentDto submitAssignmentDto,
            CancellationToken cancellationToken)
        {
            await _assignmentAppService.SubmitAsync(assignmentId ,submitAssignmentDto, cancellationToken);
            return Ok(new { Message = "Assignment submitted successfully" });
        }


        [HttpPost("{assignmentId}/grade")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Grade([FromRoute] Guid assignmentId ,[FromBody] GradeAssignmentDto dto, CancellationToken cancellationToken)
        {
            await _assignmentAppService.GradeAsync(assignmentId ,dto, cancellationToken);
            return Ok(new { Message = "Assignment graded successfully" });
        }
    }
}