namespace InterpolSystem.Test.Web.Areas.Admin.Controllers
{
    using FluentAssertions;
    using InterpolSystem.Services.Admin.Implementations;
    using InterpolSystem.Test.Mocks;
    using InterpolSystem.Web;
    using InterpolSystem.Web.Areas.Admin.Controllers;
    using InterpolSystem.Web.Areas.Admin.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UsersControllerTest
    {
        private const string FakeRole = "FakeRole";

        [Fact]
        public void UsersControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(UsersController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute
                .Should()
                .NotBeNull();

            areaAttribute.RouteValue
                .Should()
                .Be(WebConstants.AdminArea);
        }

        [Fact]
        public void UsersControllerShouldBeOnlyForAdmins()
        {
            // Arrange
            var controller = typeof(UsersController);

            // Act
            var authorizeAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            authorizeAttribute
                .Should()
                .NotBeNull();

            authorizeAttribute.Roles
                .Should()
                .Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public void UsersControllerShouldReturnUserListingViewModel()
        {
            // Arrange
            var adminUserService = new AdminUserService(Tests.GetDatabase());
            var roleManager = this.GetRoleManagerMock();
            var controller = new UsersController(adminUserService, roleManager.Object, null);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            result
                .Should()
                .NotBeNull();

            var model = result.As<ViewResult>().Model;

            model
                .Should()
                .BeOfType<UserListingsViewModel>();
        }

        private Mock<RoleManager<IdentityRole>> GetRoleManagerMock()
        {
            var roleManager = RoleManagerMock.New;
            roleManager
                .Setup(u => u.Roles)
                .Returns(this.GetIdentityRoles());

            return roleManager;
        }

        private IQueryable<IdentityRole> GetIdentityRoles()
            => new List<IdentityRole>
            {
                new IdentityRole { Name = FakeRole },
                new IdentityRole { Name = FakeRole }
            }
            .AsQueryable();
    }
}
