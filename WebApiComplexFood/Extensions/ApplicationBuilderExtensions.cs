using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApiComplexFood.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUiExtension(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectStructure.Api v1");
                options.RoutePrefix = string.Empty;
            });

    public static IApplicationBuilder UseRedirectSwaggerPath(this IApplicationBuilder app)
       => app.Use(async (context, next) =>
       {

           if(context.Request.Path == "/swagger")
           {
               context.Response.Redirect("/index.html");
               return;
           }

           await next();
       });
          


    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        var dbContext = services.ServiceProvider.GetService<ApplicationContext>();

        dbContext?.Database.Migrate();
        
    }
}