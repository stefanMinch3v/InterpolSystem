namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CountriesNationalitiesMissingConfiguration : IEntityTypeConfiguration<CountriesNationalitiesMissing>
    {
        public void Configure(EntityTypeBuilder<CountriesNationalitiesMissing> builder)
        {
            builder.HasKey(key => new { key.CountryId, key.IdentityParticularsMissingId });
        }
    }
}
