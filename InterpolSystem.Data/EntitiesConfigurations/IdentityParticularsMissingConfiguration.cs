namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class IdentityParticularsMissingConfiguration : IEntityTypeConfiguration<IdentityParticularsMissing>
    {
        public void Configure(EntityTypeBuilder<IdentityParticularsMissing> builder)
        {
            builder
                .HasOne(ipm => ipm.PhysicalDescription);

            builder
                .HasMany(ipm => ipm.Nationalities)
                .WithOne(n => n.IdentityParticularsMissing)
                .HasForeignKey(n => n.IdentityParticularsMissingId);

            builder
                .HasMany(ipm => ipm.SpokenLanguages)
                .WithOne(sl => sl.IdentityParticularsMissing)
                .HasForeignKey(sl => sl.IdentityParticularsMissingId);

            builder
                .HasMany(ipw => ipw.SubmitedForms)
                .WithOne(sf => sf.MissingPerson)
                .HasForeignKey(sf => sf.IdentityParticularsMissingId)
                .IsRequired(false);
        }
    }
}
