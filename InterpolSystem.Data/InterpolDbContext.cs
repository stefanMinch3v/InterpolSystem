namespace InterpolSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class InterpolDbContext : IdentityDbContext<User>
    {
        public InterpolDbContext(DbContextOptions<InterpolDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
