namespace InterpolSystem.Api.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Services.Blog;

    using static WebConstants;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetList()
            => Ok(this.articleService.All());

        [HttpGet(ArticleDetails)]
        [ValidateUrlId]
        public IActionResult GetArticle(int id)
            => Ok(this.articleService.ById(id));
    }
}
