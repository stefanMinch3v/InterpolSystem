namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder
                .HasMany(l => l.MissingPeopleLanguages)
                .WithOne(mpl => mpl.Language)
                .HasForeignKey(mpl => mpl.LanguageId);

            builder
                .HasMany(l => l.WantedPeopleLanguages)
                .WithOne(wpl => wpl.Language)
                .HasForeignKey(wpl => wpl.LanguageId);
        }
    }
}
