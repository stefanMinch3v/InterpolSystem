namespace InterpolSystem.Test.Web.Areas.Blog.Controllers
{
    using FluentAssertions;
    using InterpolSystem.Data.Models;
    using InterpolSystem.Services.Blog.Implementations;
    using InterpolSystem.Services.Html.Implementations;
    using InterpolSystem.Test.Mocks;
    using InterpolSystem.Web.Areas.Blog.Controllers;
    using InterpolSystem.Web.Areas.Blog.Models.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System.Linq;
    using Xunit;

    using static InterpolSystem.Web.WebConstants;

    public class ArticlesControllerTest
    {
        private const string ArticleTitle = "Article test title";
        private const string ArticleContent = "<h1>Article test content</h1>";
        private const string SuccessPublishedArticle = "Successfully published.";

        public ArticlesControllerTest()
        {
            Tests.InitializeAutoMapper();
        }

        [Fact]
        public void ArticlesControllerShouldBeInBlogArea()
        {
            // Arrange
            var controller = typeof(ArticlesController);

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
                .Be(BlogArea);
        }

        [Fact]
        public void ArticlesControllerShouldBeOnlyForBlogAdmins()
        {
            // Arrange
            var controller = typeof(ArticlesController);

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
                .Be(BloggerRole);
        }

        [Fact]
        public void ArticleIndexShouldBeForAnonymousUsers()
        {
            // Arrange
            var method = typeof(ArticlesController)
                .GetMethod(nameof(ArticlesController.Index));

            // Act
            var allowAnonymousAttribute = method.GetCustomAttributes(true)
                .FirstOrDefault(attr => attr.GetType() == typeof(AllowAnonymousAttribute));

            // Assert
            allowAnonymousAttribute
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void CreateArticlesViewShouldPassed()
        {
            // Arrange
            var articleService = new ArticleService(Tests.GetDatabase());
            var htmlService = new HtmlService();
            var userManager = this.GetUserManagerMock().Object;
            string successMsg = null;

            var tempData = new Mock<ITempDataDictionary>();
            tempData.SetupSet(t => t[TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMsg = message as string); // mock indexer

            var controller = new ArticlesController(articleService, userManager, htmlService)
            {
                TempData = tempData.Object
            };

            var publishArticleFormViewModel = new PublishArticleFormViewModel()
            {
                Title = ArticleTitle,
                Content = ArticleContent
            };

            // Act
            var resultCreateGet = controller.Create() as ViewResult;
            var resultCreatePost = controller.Create(publishArticleFormViewModel);

            // Assert
            resultCreateGet
                .Should()
                .NotBeNull();

            resultCreatePost.Should().BeOfType<RedirectToActionResult>();
            resultCreatePost.As<RedirectToActionResult>().ActionName.Should().Be(nameof(ArticlesController.Index));

            successMsg
                .Should()
                .Be(SuccessPublishedArticle);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUserId(null))
                .Returns("Fake user");

            return userManager;
        }
    }
}
