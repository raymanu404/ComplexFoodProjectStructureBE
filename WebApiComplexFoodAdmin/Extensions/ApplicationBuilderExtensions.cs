using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApiComplexFoodAdmin.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUIA(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectStructure.Api v1");
                options.RoutePrefix = string.Empty;
            });

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        var dbContext = services.ServiceProvider.GetService<ApplicationContext>();

        dbContext?.Database.Migrate();

    }


}