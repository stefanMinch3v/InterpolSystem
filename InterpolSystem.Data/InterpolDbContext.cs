namespace InterpolSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class InterpolDbContext : IdentityDbContext<User>
    {
        public InterpolDbContext(DbContextOptions<InterpolDbContext> options)
            : base(options)
        {
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // primary keys mapping tables
            builder
                .Entity<ChargesCountries>()
                .HasKey(key => new { key.ChargesId, key.CountryId });

            builder
                .Entity<CountriesNationalitiesMissing>()
                .HasKey(key => new { key.CountryId, key.IdentityParticularsMissingId });

            builder
                .Entity<CountriesNationalitiesWanted>()
                .HasKey(key => new { key.CountryId, key.IdentityParticularsWantedId });

            builder
                .Entity<LanguagesMissing>()
                .HasKey(key => new { key.LanguageId, key.IdentityParticularsMissingId });

            builder
                .Entity<LanguagesWanted>()
                .HasKey(key => new { key.LanguageId, key.IdentityParticularsWantedId });

            // charges relations
            builder
                .Entity<Charges>()
                .HasMany(ch => ch.CountryWantedAuthorities)
                .WithOne(cwa => cwa.Charges)
                .HasForeignKey(cwa => cwa.ChargesId);

            builder
                .Entity<Charges>()
                .HasOne(ch => ch.IdentityParticularsWanted)
                .WithMany(ipw => ipw.Charges)
                .HasForeignKey(ch => ch.IdentityParticularsWantedId);

            // countries relations
            builder
                .Entity<Country>()
                .HasOne(c => c.Countinent)
                .WithMany(cs => cs.Countries)
                .HasForeignKey(c => c.CountinentId);

            builder
                .Entity<Country>()
                .HasMany(c => c.ChargesCountries)
                .WithOne(cc => cc.Country)
                .HasForeignKey(cc => cc.CountryId);

            builder
               .Entity<Country>()
               .HasMany(c => c.NationalitiesMissingPeople)
               .WithOne(nmp => nmp.Country)
               .HasForeignKey(nmp => nmp.CountryId);

            builder
               .Entity<Country>()
               .HasMany(c => c.NationalitiesWantedPeople)
               .WithOne(nwp => nwp.Country)
               .HasForeignKey(nwp => nwp.CountryId);

            // missing people relations
            builder
                .Entity<IdentityParticularsMissing>()
                .HasOne(ipm => ipm.PhysicalDescription);

            builder
                .Entity<IdentityParticularsMissing>()
                .HasMany(ipm => ipm.Nationalities)
                .WithOne(n => n.IdentityParticularsMissing)
                .HasForeignKey(n => n.IdentityParticularsMissingId);

            builder
                .Entity<IdentityParticularsMissing>()
                .HasMany(ipm => ipm.SpokenLanguages)
                .WithOne(sl => sl.IdentityParticularsMissing)
                .HasForeignKey(sl => sl.IdentityParticularsMissingId);

            // wanted people relations
            builder
                .Entity<IdentityParticularsWanted>()
                .HasOne(ipw => ipw.PhysicalDescription);

            builder
                .Entity<IdentityParticularsWanted>()
                .HasMany(ipw => ipw.Nationalities)
                .WithOne(n => n.IdentityParticularsWanted)
                .HasForeignKey(n => n.IdentityParticularsWantedId);

            builder
                .Entity<IdentityParticularsWanted>()
                .HasMany(ipw => ipw.SpokenLanguages)
                .WithOne(sl => sl.IdentityParticularsWanted)
                .HasForeignKey(sl => sl.IdentityParticularsWantedId);

            // languages relations
            builder
                .Entity<Language>()
                .HasMany(l => l.MissingPeopleLanguages)
                .WithOne(mpl => mpl.Language)
                .HasForeignKey(mpl => mpl.LanguageId);

            builder
                .Entity<Language>()
                .HasMany(l => l.WantedPeopleLanguages)
                .WithOne(wpl => wpl.Language)
                .HasForeignKey(wpl => wpl.LanguageId);

            // articles relations
            builder
                .Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(au => au.Articles)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
