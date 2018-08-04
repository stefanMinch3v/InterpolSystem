namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ChargesCountriesConfiguration : IEntityTypeConfiguration<ChargesCountries>
    {
        public void Configure(EntityTypeBuilder<ChargesCountries> builder)
        {
            builder.HasKey(key => new { key.ChargesId, key.CountryId });
        }
    }
}
