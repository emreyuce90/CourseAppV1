namespace CourseApp.Domain.Entities {
    public class Course : BaseEntity {
        public string Title { get; set; }
        public string Descrption { get; set; }
        public int UserId { get; set; }
        public int Percentage { get; set; }
        public string PictureUrl { get; set; }
    }
}
