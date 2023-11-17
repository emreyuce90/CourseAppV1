using CourseApp.API.Services.Concrete;

namespace CourseApp.API.Services.Interface {
    public class UserProvider : IUserProvider {

        public int UserId => _httpContextAccessor.HttpContext?.User == null
            ? 0
            : Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value ?? "0");

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
