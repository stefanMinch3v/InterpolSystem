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
                        var configInstance = (IEntityTypeConfiguration<Article>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance);
                        break;
                    case "Charges":
                        var configInstance1 = (IEntityTypeConfiguration<Charges>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance1);
                        break;
                    case "ChargesCountries":
                        var configInstance2 = (IEntityTypeConfiguration<ChargesCountries>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance2);
                        break;
                    case "CountriesNationalitiesMissing":
                        var configInstance3 = (IEntityTypeConfiguration<CountriesNationalitiesMissing>)
                            Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance3);
                        break;
                    case "CountriesNationalitiesWanted":
                        var configInstance4 = (IEntityTypeConfiguration<CountriesNationalitiesWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance4);
                        break;
                    case "IdentityParticularsMissing":
                        var configInstance5 = (IEntityTypeConfiguration<IdentityParticularsMissing>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance5);
                        break;
                    case "IdentityParticularsWanted":
                        var configInstance6 = (IEntityTypeConfiguration<IdentityParticularsWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance6);
                        break;
                    case "Language":
                        var configInstance7 = (IEntityTypeConfiguration<Language>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance7);
                        break;
                    case "LanguagesMissing":
                        var configInstance8 = (IEntityTypeConfiguration<LanguagesMissing>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance8);
                        break;
                    case "LanguagesWanted":
                        var configInstance9 = (IEntityTypeConfiguration<LanguagesWanted>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance9);
                        break;
                    case "SubmitForm":
                        var configInstance10 = (IEntityTypeConfiguration<SubmitForm>)Activator.CreateInstance(config);
                        modelBuilder.ApplyConfiguration(configInstance10);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
