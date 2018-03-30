namespace InterpolSystem.Services.Admin.Models
{
    using System.Collections.Generic;

    public class AdminUserListingServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> RoleNames { get; set; }
    }
}
