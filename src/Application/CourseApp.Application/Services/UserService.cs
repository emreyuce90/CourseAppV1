using CourseApp.Application.Communication;
using CourseApp.Application.Dtos.User;
using CourseApp.Application.Interfaces;

namespace CourseApp.Application.Services {
    public class UserService : IUserService {
        public Task<Response> AddAsync(UserAddDto userAddDto) {
            throw new NotImplementedException();
        }

        public Task<Response> GetUserByEmailAsync(string email) {
            throw new NotImplementedException();
        }
    }
}
