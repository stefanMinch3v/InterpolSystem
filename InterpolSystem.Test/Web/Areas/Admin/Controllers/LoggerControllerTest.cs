namespace InterpolSystem.Test.Web.Areas.Admin.Controllers
{
    using FluentAssertions;
    using InterpolSystem.Services.Admin.Implementations;
    using InterpolSystem.Web.Areas.Admin.Controllers;
    using InterpolSystem.Web.Areas.Admin.Models.Logger;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static InterpolSystem.Web.WebConstants;

    public class LoggerControllerTest
    {
        public LoggerControllerTest()
        {
            Tests.InitializeAutoMapper();
        }

        [Fact]
        public void LogerControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(LoggerController);

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
                .Be(AdminArea);
        }

        [Fact]
        public void LoggerControllerShouldBeOnlyForAdmins()
        {
            // Arrange
            var controller = typeof(LoggerController);

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
                .Be(AdministratorRole);
        }

        [Fact]
        public void ShouldReturnTheCorrectViewModel()
        {
            // Arrange
            var loggerService = new LoggerService(Tests.GetDatabase());
            var controller = new LoggerController(loggerService);

            // Act
            var result = controller.All(string.Empty) as ViewResult;

            // Assert
            result
                .Should()
                .NotBeNull();

            var model = result.As<ViewResult>().Model;

            model
                .Should()
                .BeOfType<LoggerPagingViewModel>();
        }
    }
}
