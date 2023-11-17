using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IUserService _userService;

        public AuthController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserAddDto userAddDto) {
            var response = await _userService.AddAsync(userAddDto);
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
