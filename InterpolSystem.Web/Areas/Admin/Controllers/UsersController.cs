namespace InterpolSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin;
    using System.Linq;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService adminService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(
            IAdminUserService adminService, 
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.adminService = adminService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = this.adminService.All();
            var roles = this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new UserListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        // iaction result add to role
    }
}
