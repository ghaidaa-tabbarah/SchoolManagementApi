using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Enrollments;
using SchoolManagement.Application.Enrollments.DTOs;

namespace SchoolManagement.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentAppService _enrollmentAppService;

        public EnrollmentController(IEnrollmentAppService enrollmentAppService)
        {
            _enrollmentAppService = enrollmentAppService;
        }

        [HttpPost("enroll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollmentRequestDto requestDto, CancellationToken cancellationToken)
        {
            await _enrollmentAppService.EnrollStudentAsync(requestDto, cancellationToken);
            return Ok(new { Message = "Student enrolled successfully" });
        }
    }
}