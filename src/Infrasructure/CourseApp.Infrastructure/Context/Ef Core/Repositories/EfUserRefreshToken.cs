using CourseApp.Domain.Entities;
using CourseApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseApp.Infrastructure.Context.Ef_Core.Repositories {
    public class EfUserRefreshToken : EfGenericRepository<UserRefreshToken>, IUserRefreshToken {
        public EfUserRefreshToken(AppDbContext context) : base(context) {
        }
    }
}
