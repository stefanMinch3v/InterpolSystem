namespace InterpolSystem.Services.Blog
{
    using Models;
    using System.Collections.Generic;

    public interface IArticleService
    {
        void Create(string title, string content, string authorId);

        IEnumerable<ArticlesListingsServiceModel> All();

        ArticlesDetailsServiceModel ById(int id);

        IEnumerable<ArticlesListingsServiceModel> LastSixArticles();
    }
}
