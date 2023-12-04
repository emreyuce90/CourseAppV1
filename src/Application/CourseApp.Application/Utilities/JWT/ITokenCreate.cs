using CourseApp.Domain.Entities;

namespace CourseApp.Application.Utilities.JWT {
    public interface ITokenCreate {
        AccessToken CreateToken(User user);
    }
}
