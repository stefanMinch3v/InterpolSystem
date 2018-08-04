namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class LanguagesMissingConfiguration : IEntityTypeConfiguration<LanguagesMissing>
    {
        public void Configure(EntityTypeBuilder<LanguagesMissing> builder)
        {
            builder.HasKey(key => new { key.LanguageId, key.IdentityParticularsMissingId });
        }
    }
}
