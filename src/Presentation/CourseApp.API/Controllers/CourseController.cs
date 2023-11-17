using CourseApp.API.Services.Concrete;
using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.Course;
using CourseApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers {
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase {
        private readonly ICourseService _courseService;
        private readonly IUserProvider _userProvider;
        public CourseController(ICourseService courseService, IUserProvider userProvider) {
            _courseService = courseService;
            _userProvider = userProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<CourseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCoursesByUserId() {
            var response = await _courseService.GetCoursesByUserId(_userProvider.UserId);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<List<CourseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCourse(CourseAddDto courseAddDto) {
            var response = await _courseService.AddAsync(courseAddDto, _userProvider.UserId);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

    }
}
