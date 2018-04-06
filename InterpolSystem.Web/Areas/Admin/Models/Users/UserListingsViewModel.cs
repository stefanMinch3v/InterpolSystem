namespace InterpolSystem.Web.Areas.Admin.Models.Users
{
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class UserListingsViewModel : UserRolesViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
    }
}
