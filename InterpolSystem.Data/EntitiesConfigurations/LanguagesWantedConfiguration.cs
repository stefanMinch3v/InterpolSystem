namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class LanguagesWantedConfiguration : IEntityTypeConfiguration<LanguagesWanted>
    {
        public void Configure(EntityTypeBuilder<LanguagesWanted> builder)
        {
            builder.HasKey(key => new { key.LanguageId, key.IdentityParticularsWantedId });
        }
    }
}
