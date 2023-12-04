
using CourseApp.API.Extensions;
using CourseApp.API.Services.Concrete;
using CourseApp.API.Services.Interface;
using CourseApp.Application.Extensions.MicrosoftIoC;
using CourseApp.Cache;
using CourseApp.Infrastructure.Extensions.MicrosoftIoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CourseApp.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Course App Token Based Auth Project",
                    Description = "Course App Token Based Auth Project",
                    TermsOfService = new Uri("https://www.linkedin.com/in/mreyuce/"),
                    Contact = new OpenApiContact {
                        Email = "emreyuce9039@gmail.com",
                        Name = "Emre Yüce",
                        Url = new Uri("https://www.linkedin.com/in/mreyuce/")
                    },
                    License = new OpenApiLicense { Name = "Emre Yüce Licence", Url = new Uri("https://www.linkedin.com/in/mreyuce/") }

                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}
                        }
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddInfrastructureDependencies(builder.Configuration);
            builder.Services.AddApplicationDependencies();
            builder.Services.AddCors(options => options.AddDefaultPolicy(
            policy => policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod()));

            builder.Services.AddScoped<IUserProvider, UserProvider>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options => options.TokenValidationParameters = new() {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidAudience = builder.Configuration["Token:Audience"],
                ValidIssuer = builder.Configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
            });
            builder.Services.AddSingleton<RedisService>(sp => {
                return new RedisService(builder.Configuration["Redis:Url"]);
            });
            var app = builder.Build();



            app.UseSwagger();
            app.UseSwaggerUI();

            app.ConfigureExceptionHandler();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
