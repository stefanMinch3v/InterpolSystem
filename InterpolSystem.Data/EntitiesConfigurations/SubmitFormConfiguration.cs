namespace InterpolSystem.Data.EntitiesConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SubmitFormConfiguration : IEntityTypeConfiguration<SubmitForm>
    {
        public void Configure(EntityTypeBuilder<SubmitForm> builder)
        {
            builder
                .HasOne(f => f.User)
                .WithMany(u => u.SubmitForms)
                .HasForeignKey(f => f.UserId)
                .IsRequired(false);
        }
    }
}
