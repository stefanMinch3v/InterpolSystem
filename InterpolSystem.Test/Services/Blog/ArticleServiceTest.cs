namespace InterpolSystem.Test.Services.Blog
{
    using FluentAssertions;
    using InterpolSystem.Data.Models;
    using InterpolSystem.Services.Blog.Implementations;
    using InterpolSystem.Services.Blog.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    using static InterpolSystem.Services.ServiceConstants;

    public class ArticleServiceTest
    {
        private const string ArticleTitle = "Test article title";
        private const string ArticleContent = "Test article content";

        public ArticleServiceTest()
        {
            Tests.InitializeAutoMapper();
        }

        [Fact]
        public void CreateArticleShouldReturnTheCorrectResult()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            // Act
            var authorFakeId = Guid.NewGuid().ToString();
            articleService.Create(ArticleTitle, ArticleContent, authorFakeId);

            var result = db.Articles.FirstOrDefault();

            // Assert
            result.Title
                .Should().Be(ArticleTitle);
        }

        [Fact]
        public void CreateArticleShouldThrowExceptionWithEmptyAuthorId()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            // Act
            Action act = () => articleService.Create(ArticleTitle, ArticleContent, string.Empty);
            //var result = Record.Exception(act); xunit feature

            // Assert
            act
                .Should().ThrowExactly<InvalidOperationException>().WithMessage(InvalidInsertedData);
        }

        [Fact]
        public void ShouldReturnExactlyTwoArticles()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            var articleData = this.GetArticleData();

            db.AddRange(articleData);
            db.SaveChanges();

            // Act
            var result = articleService.All();

            // Assert
            result
                .Should().AllBeOfType<ArticlesListingsServiceModel>()
                .And.HaveCount(2);
        }

        [Fact]
        public void FindArticleByIdShouldReturnTheCorrectResult()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            var articleData = this.GetArticleData();

            db.AddRange(articleData);
            db.SaveChanges();

            // Act
            var result = articleService.ById(1);

            // Assert
            result.Title
               .Should().Be(ArticleTitle);

            result
                .Should().BeOfType<ArticlesDetailsServiceModel>();
        }

        [Fact]
        public void FindArticleByIdShouldReturnNullWithUnexistingId()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            var articleData = this.GetArticleData();

            db.AddRange(articleData);
            db.SaveChanges();

            // Act
            var result = articleService.ById(11);

            // Assert
            result
               .Should().BeNull();
        }

        [Fact]
        public void ShouldReturnTheLastSixArticles()
        {
            // Arrange
            var db = Tests.GetDatabase();
            var articleService = new ArticleService(db);

            var articleData = this.GetEightArticles();

            db.AddRange(articleData);
            db.SaveChanges();

            // Act
            var result = articleService.LastSixArticles();

            // Assert
            result
               .Should().NotContain(r => r.Id < 3);

            result
               .Should().HaveCount(6)
               .And.AllBeOfType<ArticlesListingsServiceModel>();
        }

        private IEnumerable<Article> GetArticleData()
        {
            var authorFakeId = Guid.NewGuid().ToString();

            var articleOne = new Article
            {
                Id = 1,
                Title = ArticleTitle,
                Content = ArticleContent,
                AuthorId = authorFakeId
            };

            var articleTwo = new Article
            {
                Id = 2,
                Title = ArticleTitle + "2",
                Content = ArticleContent + "2",
                AuthorId = authorFakeId + "2"
            };

            return new List<Article> { articleOne, articleTwo };
        }

        private IEnumerable<Article> GetEightArticles()
        {
            var list = new List<Article>();
            var authorFakeId = Guid.NewGuid().ToString();

            for (int i = 1; i <= 8; i++)
            {
                var article = new Article
                {
                    Id = i,
                    Title = ArticleTitle + i,
                    Content = ArticleContent + i,
                    AuthorId = authorFakeId + i
                };

                list.Add(article);
            }

            return list;
        }
    }
}
