using CourseApp.Application.Interfaces;
using CourseApp.Application.Mappings;
using CourseApp.Application.Services;
using CourseApp.Application.Utilities.JWT;
using CourseApp.Application.ValidationRules.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CourseApp.Application.Extensions.MicrosoftIoC {
    public static class ApplicationDependencies {
        public static void AddApplicationDependencies(this IServiceCollection services) {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            services.AddScoped<ITokenCreate, TokenCreate>();
            services.AddScoped<IAuthService, AuthService>();
            #region Validations Register 

            services.AddValidatorsFromAssemblyContaining(typeof(UserAddDtoValidator));
            var serviceDescriptors = services
                .Where(descriptor => typeof(IValidator) != descriptor.ServiceType
                       && typeof(IValidator).IsAssignableFrom(descriptor.ServiceType)
                       && descriptor.ServiceType.IsInterface)
                .ToList();

            foreach (var descriptor in serviceDescriptors) {
                services.Add(new ServiceDescriptor(
                    typeof(IValidator),
                    p => p.GetRequiredService(descriptor.ServiceType),
                    descriptor.Lifetime));
            }

            #endregion Validations Register
        }
    }
}
