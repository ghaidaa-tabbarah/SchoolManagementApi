using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Grades;
using SchoolManagement.Application.Grades.DTOs;

namespace SchoolManagement.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeAppService _gradeAppService;

        public GradeController(IGradeAppService gradeAppService)
        {
            _gradeAppService = gradeAppService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GradeRequestDto requestDto, CancellationToken cancellationToken)
        {
            var grades = await _gradeAppService.GetAllAsync(requestDto, cancellationToken);
            return Ok(grades);
        }
    }
}