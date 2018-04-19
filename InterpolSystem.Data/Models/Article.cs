namespace InterpolSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ArticlesTitleMaxLength, MinimumLength = ArticlesTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ArticlesContentMaxLength, MinimumLength = ArticlesContentMinLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
