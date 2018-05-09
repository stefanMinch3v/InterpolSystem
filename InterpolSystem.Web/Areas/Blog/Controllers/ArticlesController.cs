namespace InterpolSystem.Web.Areas.Blog.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Blog;
    using Services.Html;
    using System;

    using static WebConstants;

    [Area(BlogArea)]
    [Authorize(Roles = BloggerRole + ", " + AdministratorRole)]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly UserManager<User> userManager;
        private readonly IHtmlService htmlService;

        public ArticlesController(
            IArticleService articleService,
            UserManager<User> userManager,
            IHtmlService htmlService)
            
        {
            this.articleService = articleService;
            this.userManager = userManager;
            this.htmlService = htmlService;
        }

        [AllowAnonymous]
        public IActionResult Index()
            => View(this.articleService.All());

        [AllowAnonymous]
        public IActionResult Details(int id)
           => this.ViewOrNotFound(this.articleService.ById(id));

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(PublishArticleFormViewModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            if (userId == null)
            {
                TempData.AddErrorMessage("The author does not exist.");
                return View();
            }

            var sanitizedContent = this.htmlService.Sanitize(model.Content);

            try
            {
                this.articleService.Create(model.Title, sanitizedContent, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            TempData.AddSuccessMessage("Successfully published.");

            return RedirectToAction(nameof(Index));
        }
    }
}
