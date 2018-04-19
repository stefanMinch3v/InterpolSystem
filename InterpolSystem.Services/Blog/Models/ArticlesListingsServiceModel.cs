namespace InterpolSystem.Services.Blog.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ArticlesListingsServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorName { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, ArticlesListingsServiceModel>()
                .ForMember(s => s.AuthorName, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
