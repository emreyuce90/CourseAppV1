using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace CourseApp.API.Extensions {
    public static class ConfigureExcepitonHandlerExtension {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseExceptionHandler(builder => {
                builder.Run(async context => {
                    var logger = app.ApplicationServices.GetService<ILogger<Program>>();

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null) {
                        logger?.LogError($"Message: {exceptionHandlerFeature.Error.Message}\n" +
                                         $"Endpoint: {context.GetEndpoint()?.DisplayName}\n" +
                                         $"Path: {context.Request.Path}");

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new {
                            StatusCode = context.Response.StatusCode,
                            ContentType = MediaTypeNames.Application.Json,
                            Message = exceptionHandlerFeature.Error.Message,
                            Title = "Global Hata Yakalayıcısı ile yakalandı"
                        }));
                    }
                });
            });
        }


    }
}
