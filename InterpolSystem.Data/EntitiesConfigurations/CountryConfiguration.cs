namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasOne(c => c.Countinent)
                .WithMany(cs => cs.Countries)
                .HasForeignKey(c => c.CountinentId);

            builder
                .HasMany(c => c.ChargesCountries)
                .WithOne(cc => cc.Country)
                .HasForeignKey(cc => cc.CountryId);

            builder
               .HasMany(c => c.NationalitiesMissingPeople)
               .WithOne(nmp => nmp.Country)
               .HasForeignKey(nmp => nmp.CountryId);

            builder
               .HasMany(c => c.NationalitiesWantedPeople)
               .WithOne(nwp => nwp.Country)
               .HasForeignKey(nwp => nwp.CountryId);
        }
    }
}
