namespace InterpolSystem.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class UserRolesViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
