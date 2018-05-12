namespace InterpolSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.MissingPeople;
    using Models.Shared;
    using Services;
    using Services.Blog;
    using Services.BountyAdmin;
    using System;

    using static WebConstants;

    public class MissingPeopleController : BasePeopleController
    {
        private readonly IArticleService articleService;
        private readonly IMissingPeopleService peopleService;

        public MissingPeopleController(
            IArticleService articleService,
            IMissingPeopleService peopleService,
            IBountyAdminService bountyAdminService)
            : base(bountyAdminService)
        {
            this.peopleService = peopleService;
            this.articleService = articleService;
        }

        public IActionResult Index(int page = 1)
            => View(new MissingPeoplePageListingModel
            {
                MissingPeople = this.peopleService.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.peopleService.Total() / (double)PageSize),
                Countries = this.GetCountries(),
                Articles = articleService.LastSixArticles()
            });

        public IActionResult Details(int id)
        {
            var existingPerson = this.peopleService.IsPersonExisting(id);

            if (!existingPerson)
            {
                return BadRequest();
            }

            var person = this.peopleService.GetPerson(id);

            return View(person);
        }

        public IActionResult Search(SearchFormViewModel model, int page = 1)
            => View(new MissingPeoplePageListingModel
            {
                CurrentPage = page,
                MissingPeople = this.peopleService.SearchByComponents(
                    model.EnableCountrySearch,
                    model.SelectedCountryId ?? 0,
                    model.EnableGenderSearch,
                    model.SelectedGender,
                    model.SearchByFirstName,
                    model.SearchByLastName,
                    model.SearchByDistinguishMarks,
                    model.SearchByAge ?? 0,
                    page,
                    PageSize),
                SearchCriteriaTotalPages = this.peopleService.SearchPeopleCriteriaCounter,
                TotalPages = (int)Math.Ceiling(this.peopleService.SearchPeopleCriteriaCounter / (double)PageSize)
            });
    }
}
