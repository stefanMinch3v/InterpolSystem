namespace InterpolSystem.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class UserListingsViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
    }
}
