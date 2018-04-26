namespace InterpolSystem.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin;
    using System;
    using System.Collections.Generic;
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
            => View(new UserListingsViewModel
            {
                Users = this.adminService.All(),
                Roles = this.GetRoles()
            });

        [LogEmployees]
        public IActionResult Create()
            => View(new CreateUserFormViewModel
            {
                Roles = this.GetRoles()
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(this.ReturnUserFormViewModelWithErrors(model));
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var password = this.GenerateRandomPassword();
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                TempData[TempDataErrorMessageKey] = $"User with username {model.UserName} or email {model.Email} already exists.";
                return View(this.ReturnUserFormViewModelWithErrors(model));
            }

            if (!model.WithoutRole)
            {
                var existingRole = await this.roleManager.RoleExistsAsync(model.Role);

                if (!existingRole)
                {
                    ModelState.AddModelError(string.Empty, "Invalid role.");
                }

                await this.userManager.AddToRoleAsync(user, model.Role);
            }

            TempData[TempDataSuccessMessageKey] = $"Successfully created with password: {password}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [LogEmployees]
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var existingUser = await this.userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var isUserAdmin = await this.userManager.IsInRoleAsync(existingUser, AdministratorRole);

            if (isUserAdmin)
            {
                ModelState.AddModelError(string.Empty, "Invalid operation.");
            }

            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Cannot modify other administrator.");
                return RedirectToAction(nameof(Index));
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(existingUser);
            var newPassword = this.GenerateRandomPassword();

            var result = await this.userManager.ResetPasswordAsync(existingUser, token, newPassword);

            if (!result.Succeeded)
            {
                return BadRequest("Failed to reset the password.");
            }

            this.TempData.AddSuccessMessage($"Successfully reset the password. The new password is: {newPassword}");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [LogEmployees]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var existingUser = await this.userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            var isUserAdmin = await this.userManager.IsInRoleAsync(existingUser, AdministratorRole);

            if (isUserAdmin)
            {
                ModelState.AddModelError(string.Empty, "Invalid operation.");
            }

            if (!ModelState.IsValid)
            {
                this.TempData.AddErrorMessage("Cannot delete administrator.");
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.DeleteAsync(existingUser);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [LogEmployees]
        public async Task<IActionResult> AddToRole(AddRemoveUserToRoleViewModel model)
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
        [LogEmployees]
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

        private List<SelectListItem> GetRoles()
            => this.roleManager.Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

        private CreateUserFormViewModel ReturnUserFormViewModelWithErrors(CreateUserFormViewModel model)
            => new CreateUserFormViewModel
            {
                Roles = this.GetRoles(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                WithoutRole = model.WithoutRole
            };

        private string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null)
            {
                // by default
                opts = new PasswordOptions()
                {
                    RequiredLength = 10,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true
                };
            }
            
            string[] randomChars = new[] 
            {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "@$"                            // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            var chars = new List<char>();

            if (opts.RequireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            }

            if (opts.RequireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
            }
                
            if (opts.RequireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            }

            if (opts.RequireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
            }
               
            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
