namespace CourseApp.Application.Utilities.JWT {
    public class AccessToken {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
