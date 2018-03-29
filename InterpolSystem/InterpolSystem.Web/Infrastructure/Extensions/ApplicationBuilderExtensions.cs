namespace InterpolSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<InterpolDbContext>()
                    .Database
                    .Migrate();
            }

            return app;
        }
    }
}
