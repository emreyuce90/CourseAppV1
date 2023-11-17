namespace CourseApp.Domain.Entities {
    public class Course : BaseEntity {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string PictureUrl { get; set; }
    }
}
