using CourseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Infrastructure.Context {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
