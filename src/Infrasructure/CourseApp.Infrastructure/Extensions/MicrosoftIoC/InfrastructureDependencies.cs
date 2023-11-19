using CourseApp.Domain.Interfaces;
using CourseApp.Infrastructure.Context;
using CourseApp.Infrastructure.Context.Ef_Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApp.Infrastructure.Extensions.MicrosoftIoC {
    public static class InfrastructureDependencies {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration) {
            //services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<ICourseRepository, EfCourseRepository>();
        }
    }
}
