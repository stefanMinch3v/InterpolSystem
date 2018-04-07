namespace InterpolSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // db migration
                serviceScope
                    .ServiceProvider
                    .GetService<InterpolDbContext>()
                    .Database
                    .Migrate();

                var userManager = serviceScope
                    .ServiceProvider
                    .GetService<UserManager<User>>();

                var roleManager = serviceScope
                    .ServiceProvider
                    .GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    // add roles
                    var roles = new[]
                    {
                        AdministratorRole,
                        TestRole,
                        WantedMissingPeopleRole
                    };

                    foreach (var role in roles)
                    {
                        var existingRole = await roleManager.RoleExistsAsync(role);

                        if (!existingRole)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = role
                            });
                        }
                    }

                    // add administrator 
                    var adminUsername = "admin";
                    var adminEmail = "admin@mymail.com";
                    var adminFromDb = await userManager.FindByEmailAsync(adminEmail);

                    if (adminFromDb == null)
                    {
                        var user = new User
                        {
                            UserName = adminUsername,
                            Email = adminEmail,
                            FirstName = adminUsername,
                            LastName = adminUsername,
                            DateOfBirth = DateTime.UtcNow
                        };

                        await userManager.CreateAsync(user, "admin12");

                        await userManager.AddToRoleAsync(user, AdministratorRole);
                    }
                })
                .Wait();
            }

            return app;
        }
    }
}
