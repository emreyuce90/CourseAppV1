using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;

namespace CourseApp.Application.Interfaces {
    public interface IAuthService {
        Task<Response> CreateToken(UserDto userDto);
        Task<Response> VerifyUser(UserLoginDto userLoginDto);
    }
}
