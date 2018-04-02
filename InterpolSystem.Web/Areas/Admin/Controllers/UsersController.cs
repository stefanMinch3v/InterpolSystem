namespace InterpolSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

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

        [HttpPost]
        public async Task<IActionResult> AddToRole(/*string buttonValue*/AddRemoveUserToRoleViewModel model)
        {
            var existingRole = await this.roleManager.RoleExistsAsync(model.Role);
            var selectedUser = await this.userManager.FindByIdAsync(model.UserId);
            var existingUser = selectedUser != null;

            if (!existingRole || !existingUser)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Either user or role are not valid.");
                return RedirectToAction(nameof(Index));
            }

            var result = await this.userManager.AddToRoleAsync(selectedUser, model.Role);

            if (!result.Succeeded)
            {
                this.TempData.AddErrorMessage($"{selectedUser.UserName} is already in role {model.Role}");
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage($"User {selectedUser.UserName} successfully added to role {model.Role}.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(AddRemoveUserToRoleViewModel model)
        {
            var existingRole = await this.roleManager.RoleExistsAsync(model.Role);
            var selectedUser = await this.userManager.FindByIdAsync(model.UserId);
            var isUserInRole = await this.userManager.IsInRoleAsync(selectedUser, model.Role);
            var existingUser = selectedUser != null;

            if (!existingRole || !existingUser || !isUserInRole)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Either user or role are not valid.");
                return RedirectToAction(nameof(Index));
            }

            var result = await this.userManager.RemoveFromRoleAsync(selectedUser, model.Role);

            if (!result.Succeeded)
            {
                this.TempData.AddErrorMessage($"{selectedUser.UserName} is not in role {model.Role}");
                return RedirectToAction(nameof(Index));
            }

            this.TempData.AddSuccessMessage($"User {selectedUser.UserName} successfully removed from role {model.Role}.");
            return RedirectToAction(nameof(Index));
        }
    }
}
