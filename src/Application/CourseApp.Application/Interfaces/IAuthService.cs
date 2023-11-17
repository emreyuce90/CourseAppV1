using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;

namespace CourseApp.Application.Interfaces {
    public interface IAuthService {
        Task<Response> CreateToken(UserLoginDto userLoginDto);
        Task<Response> VerifyUser(UserLoginDto userLoginDto);
    }
}
