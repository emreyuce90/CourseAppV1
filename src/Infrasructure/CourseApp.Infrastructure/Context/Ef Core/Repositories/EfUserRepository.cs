using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;

namespace CourseApp.Infrastructure.Context.Ef_Core.Repositories {
    public class EfUserRepository : EfGenericRepository<User>, IUserRepository {
        public EfUserRepository(AppDbContext context) : base(context) {
        }
    }
}
