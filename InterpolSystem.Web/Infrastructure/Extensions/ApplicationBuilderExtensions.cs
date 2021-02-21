namespace InterpolSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Models;
    using InterpolSystem.Languages;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
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

                var dbContext = serviceScope
                    .ServiceProvider
                    .GetService<InterpolDbContext>();

                if (!dbContext.Countinents.Any())
                {
                    var languageService = serviceScope
                        .ServiceProvider
                        .GetService<ILanguageService>();

                    var continets = languageService.GetContinents()
                        .Select(c => new Countinent
                        {
                            Name = c.Name,
                            Code = c.Code
                        });

                    var languages = languageService.GetLanguages()
                        .Select(c => new Language
                        {
                            Name = c.Name
                        });

                    var countries = languageService.GetCountries()
                        .Select(c => new Country
                        {
                            Name = c.Name,
                            Code = c.Code,
                            Countinent = continets.First(con => con.Code == languageService.GetContinentCode(c.Code))
                        });

                    dbContext.Countinents.AddRange(continets);
                    dbContext.Countries.AddRange(countries);
                    dbContext.Languages.AddRange(languages);
                    dbContext.SaveChanges();
                }

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
                        WantedMissingPeopleRole,
                        BloggerRole,
                        BountyHunterRole
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
