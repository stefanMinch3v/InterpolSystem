namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class IdentityParticularsWantedConfiguration : IEntityTypeConfiguration<IdentityParticularsWanted>
    {
        public void Configure(EntityTypeBuilder<IdentityParticularsWanted> builder)
        {
            builder
                .HasOne(ipw => ipw.PhysicalDescription);

            builder
                .HasMany(ipw => ipw.Nationalities)
                .WithOne(n => n.IdentityParticularsWanted)
                .HasForeignKey(n => n.IdentityParticularsWantedId);

            builder
                .HasMany(ipw => ipw.SpokenLanguages)
                .WithOne(sl => sl.IdentityParticularsWanted)
                .HasForeignKey(sl => sl.IdentityParticularsWantedId);

            builder
                .HasMany(ipw => ipw.SubmitedForms)
                .WithOne(sf => sf.WantedPerson)
                .HasForeignKey(sf => sf.IdentityParticularsWantedId)
                .IsRequired(false);
        }
    }
}
