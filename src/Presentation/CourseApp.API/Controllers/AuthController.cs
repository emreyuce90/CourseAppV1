using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService) {
            _userService = userService;
            _authService = authService;
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

        [HttpPost]
        [ProducesResponseType(typeof(Response<UserResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto) {
            var response = await _authService.VerifyUser(userLoginDto);
            if (!response.Success) return BadRequest(response);
            var tokenResponse = await _authService.CreateToken(userLoginDto);
            if (!tokenResponse.Success) return BadRequest(tokenResponse);

            return Ok(tokenResponse);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Response<UserResource>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTokenByRefreshToken(string refreshToken) {
            var response = await _authService.CreateTokenByRefreshToken(refreshToken);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
