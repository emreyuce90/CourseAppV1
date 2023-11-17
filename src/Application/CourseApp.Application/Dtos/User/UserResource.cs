using CourseApp.Application.Utilities.JWT;

namespace CourseApp.Application.Dtos.User {
    public class UserResource {
        public AccessToken Token { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
