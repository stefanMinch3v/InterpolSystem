namespace InterpolSystem.Data.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ModelBuilderExtensions
    {
        private const string ConfigName = "Configuration";
        private const string MethodName = "Configure";

        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder modelBuilder)
        {
            // get all configuration entities
            var configs = Assembly
                .GetAssembly(typeof(IData))
                .GetTypes()
                .Where(t => t.IsClass && t.FullName.EndsWith(ConfigName))
                .ToList();

            // get all entity classes
            var models = Assembly
                .GetAssembly(typeof(IData))
                .GetTypes()
                .Where(t => t.IsClass)
                .ToList();

            foreach (var config in configs)
            {
                var entityName = config.Name.Substring(0, config.Name.Length - ConfigName.Length);
                var model = models.FirstOrDefault(m => m.Name == entityName);

                if (model == null)
                {
                    return;
                }

                switch (model.Name)
                {
                    case "Article":
                        var configInstanceArticle = (IEntityTypeConfiguration<Article>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceArticle);
                        break;
                    case "Charges":
                        var configInstanceCharges = (IEntityTypeConfiguration<Charges>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceCharges);
                        break;
                    case "ChargesCountries":
                        var configInstanceChargesCountries = (IEntityTypeConfiguration<ChargesCountries>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceChargesCountries);
                        break;
                    case "CountriesNationalitiesMissing":
                        var configInstanceCountriesNationalitiesMissing = (IEntityTypeConfiguration<CountriesNationalitiesMissing>)
                            Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceCountriesNationalitiesMissing);
                        break;
                    case "CountriesNationalitiesWanted":
                        var configInstanceCountriesNationalitiesWanted = (IEntityTypeConfiguration<CountriesNationalitiesWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceCountriesNationalitiesWanted);
                        break;
                    case "IdentityParticularsMissing":
                        var configInstanceIdentityParticularsMissing = (IEntityTypeConfiguration<IdentityParticularsMissing>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceIdentityParticularsMissing);
                        break;
                    case "IdentityParticularsWanted":
                        var configInstanceIdentityParticularsWanted = (IEntityTypeConfiguration<IdentityParticularsWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceIdentityParticularsWanted);
                        break;
                    case "Language":
                        var configInstanceLanguage = (IEntityTypeConfiguration<Language>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceLanguage);
                        break;
                    case "LanguagesMissing":
                        var configInstanceLanguagesMissing = (IEntityTypeConfiguration<LanguagesMissing>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceLanguagesMissing);
                        break;
                    case "LanguagesWanted":
                        var configInstanceLanguagesWanted = (IEntityTypeConfiguration<LanguagesWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceLanguagesWanted);
                        break;
                    case "SubmitForm":
                        var configInstanceSubmitForm = (IEntityTypeConfiguration<SubmitForm>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstanceSubmitForm);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
