namespace CourseApp.Domain.Entities {
    public class User : BaseEntity {
        public User() {
            Courses = new List<Course>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public List<Course> Courses { get; set; }
    }
}
