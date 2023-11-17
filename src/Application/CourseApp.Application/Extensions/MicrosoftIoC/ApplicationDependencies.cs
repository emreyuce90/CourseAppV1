using CourseApp.Application.Interfaces;
using CourseApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApp.Application.Extensions.MicrosoftIoC {
    public static class ApplicationDependencies {
        public static void AddApplicationDependencies(this IServiceCollection services) {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
        }
    }
}
