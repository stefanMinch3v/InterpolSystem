namespace InterpolSystem.Data
{
    using EntitiesConfigurations;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class InterpolDbContext : IdentityDbContext<User>
    {
        public InterpolDbContext(DbContextOptions<InterpolDbContext> options)
            : base(options)
        {
            // this.Database.SetCommandTimeout(60); // ucn server kraka is not one of the fastest ones so we get timeout exception for very simple queries and the temporary solution could be to increase the waiting time. Bottleneck when use in memory database for testing throws exception cuz of the timeout ....
        }

        public DbSet<Charges> Charges { get; set; }

        public DbSet<Countinent> Countinents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<IdentityParticularsMissing> IdentityParticularsMissing { get; set; }

        public DbSet<IdentityParticularsWanted> IdentityParticularsWanted { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<PhysicalDescription> PhysicalDescriptions { get; set; }

        public DbSet<ChargesCountries> ChargesCountries { get; set; }

        public DbSet<CountriesNationalitiesMissing> CountriesNationalitiesMissing { get; set; }

        public DbSet<CountriesNationalitiesWanted> CountriesNationalitiesWanted { get; set; }

        public DbSet<LanguagesMissing> LanguagesMissing { get; set; }

        public DbSet<LanguagesWanted> LanguagesWanted { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<SubmitForm> SubmitForms { get; set; }

        public DbSet<LogEmployee> LogEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // reflection for adding all the configurations below but its commented in order to track all the relations.
            // builder.ApplyAllConfigurationsFromCurrentAssembly();

            // primary keys mapping tables
            builder.ApplyConfiguration(new ChargesCountriesConfiguration());
            builder.ApplyConfiguration(new CountriesNationalitiesMissingConfiguration());
            builder.ApplyConfiguration(new CountriesNationalitiesWantedConfiguration());
            builder.ApplyConfiguration(new LanguagesMissingConfiguration());
            builder.ApplyConfiguration(new LanguagesWantedConfiguration());

            // charges relations
            builder.ApplyConfiguration(new ChargesConfiguration());

            // countries relations
            builder.ApplyConfiguration(new CountryConfiguration());

            // missing people relations
            builder.ApplyConfiguration(new IdentityParticularsMissingConfiguration());

            // wanted people relations
            builder.ApplyConfiguration(new IdentityParticularsWantedConfiguration());

            // languages relations
            builder.ApplyConfiguration(new LanguageConfiguration());

            // articles relations
            builder.ApplyConfiguration(new ArticleConfiguration());

            // submit form relations to be changed to many to many
            builder.ApplyConfiguration(new SubmitFormConfiguration());
        }
    }
}
