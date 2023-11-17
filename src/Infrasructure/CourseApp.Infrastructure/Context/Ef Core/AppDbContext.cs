using CourseApp.Domain.Entities;
using CourseApp.Infrastructure.Context.Ef_Core.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Infrastructure.Context {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
