using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;

namespace CourseApp.Application.Interfaces {
    public interface IUserService {
        Task<Response> AddAsync(UserAddDto userAddDto);
        Task<Response> GetUserByEmailAsync(string email);
    }
}
