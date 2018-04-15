namespace InterpolSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.MissingPeople;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MissingPeopleController : Controller
    {
        private const int PageSize = 6;

        private readonly IMissingPeopleService peopleService;

        public MissingPeopleController(IMissingPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        public IActionResult Index(int page = 1)
            => View(new MissingPeoplePageListingModel
            {
                MissingPeople = this.peopleService.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.peopleService.Total() / (double)PageSize),
                Countries = this.GetCountries()
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
        {
            var viewModel = new MissingPeoplePageListingModel
            {
                CurrentPage = page
            };

            viewModel.MissingPeople = this.peopleService.SearchByComponents(
                model.EnableCountrySearch,
                model.SelectedCountryId ?? 0,
                model.EnableGenderSearch,
                model.SelectedGender,
                model.SearchByFirstName,
                model.SearchByLastName,
                model.SearchByDistinguishMarks,
                model.SearchByAge ?? 0);

            var totalResults = viewModel.MissingPeople.Any() ? viewModel.MissingPeople.Count() : 0;

            viewModel.TotalPages = (int)Math.Ceiling(totalResults / (double)PageSize);

            return View(viewModel);
        }

        private List<SelectListItem> GetCountries()
            => this.peopleService.GetCountriesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();
    }
}
