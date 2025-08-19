using BeautySalon.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text.Json;

namespace BeautySalon.RestApi.Configurations.Exceptions;

public static class CustomeExceptionHandler
{
    public static IApplicationBuilder UseCustomExceptionHandler (this IApplicationBuilder app)
    {
        var environment = app.ApplicationServices
              .GetRequiredService<IWebHostEnvironment>();

        var jsonOptions = app.ApplicationServices
            .GetService<IOptions<JsonOptions>>()?.Value.SerializerOptions;

        app.UseExceptionHandler(_ => _.Run(async context =>
        {
            var exception = context.Features
                .Get<IExceptionHandlerPathFeature>()?.Error;

            var isAssignToCustomException = exception?
                .GetType()
                .IsAssignableTo(typeof(CustomException));

            const string errorProduction = "UnknownError";

            var result = new ExceptionErrorDto();

            result.StatusCode = context.Response.StatusCode;

            if (!environment.IsDevelopment())
            {
                if (exception is CustomException)
                {
                    result.Error = exception?.GetType()
                         .Name.Replace("Exception", string.Empty);
                    result.Description = null;
                }
                else
                {
                    result.Error = errorProduction;
                    result.Description = exception?.ToString();
                }
            }
            else
            {
                result.Error = exception?.GetType().Name.Replace("Exception", string.Empty);
                result.Description = exception?.ToString();
            }
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
        }));

        if (environment.IsDevelopment()) app.UseHsts();

        return app;
    }
}
