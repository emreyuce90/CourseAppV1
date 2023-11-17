namespace CourseApp.Domain.Entities {
    public class User : BaseEntity {
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
    }
}
