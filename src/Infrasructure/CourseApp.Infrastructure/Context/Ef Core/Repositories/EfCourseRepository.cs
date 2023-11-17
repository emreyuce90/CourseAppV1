using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;

namespace CourseApp.Infrastructure.Context.Ef_Core.Repositories {
    public class EfCourseRepository : EfGenericRepository<Course>, ICourseRepository {
        public EfCourseRepository(AppDbContext context) : base(context) {
        }
    }
}
