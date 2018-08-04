namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ChargesConfiguration : IEntityTypeConfiguration<Charges>
    {
        public void Configure(EntityTypeBuilder<Charges> builder)
        {
            builder
                .HasMany(ch => ch.CountryWantedAuthorities)
                .WithOne(cwa => cwa.Charges)
                .HasForeignKey(cwa => cwa.ChargesId);

            builder
                .HasOne(ch => ch.IdentityParticularsWanted)
                .WithMany(ipw => ipw.Charges)
                .HasForeignKey(ch => ch.IdentityParticularsWantedId);
        }
    }
}
