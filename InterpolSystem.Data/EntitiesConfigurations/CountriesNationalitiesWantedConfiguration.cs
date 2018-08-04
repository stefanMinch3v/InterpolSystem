namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CountriesNationalitiesWantedConfiguration : IEntityTypeConfiguration<CountriesNationalitiesWanted>
    {
        public void Configure(EntityTypeBuilder<CountriesNationalitiesWanted> builder)
        {
            builder.HasKey(key => new { key.CountryId, key.IdentityParticularsWantedId });
        }
    }
}
