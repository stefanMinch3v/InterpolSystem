namespace InterpolSystem.Web.Areas.Blog.Models.Articles
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PublishArticleFormViewModel
    {
        [Required]
        [StringLength(ArticlesTitleMaxLength, MinimumLength = ArticlesTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ArticlesContentMaxLength, MinimumLength = ArticlesContentMinLength)]
        public string Content { get; set; }
    }
}
