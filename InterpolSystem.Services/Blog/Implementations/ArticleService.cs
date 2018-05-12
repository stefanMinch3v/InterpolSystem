namespace InterpolSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static ServiceConstants;

    public class ArticleService : IArticleService
    {
        private readonly InterpolDbContext db;

        public ArticleService(InterpolDbContext db)
        {
            this.db = db;
        }

        public void Create(string title, string content, string authorId)
        {
            if (string.IsNullOrEmpty(title)
                || string.IsNullOrEmpty(content)
                || string.IsNullOrEmpty(authorId))
            {
                throw new InvalidOperationException(InvalidInsertedData);
            }

            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Articles.Add(article);
            this.db.SaveChanges();
        }

        public IEnumerable<ArticlesListingsServiceModel> All()
            => this.db.Articles
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticlesListingsServiceModel>()
                .ToList();

        public ArticlesDetailsServiceModel ById(int id)
            => this.db.Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticlesDetailsServiceModel>()
                .FirstOrDefault();

        public IEnumerable<ArticlesListingsServiceModel> LastSixArticles()
            => this.db.Articles
                .OrderByDescending(a => a.Id)
                .Take(6)
                .ProjectTo<ArticlesListingsServiceModel>()
                .ToList();
    }
}
