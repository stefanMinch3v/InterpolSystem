namespace InterpolSystem.Web.Areas.WantedAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.WantedPeople;
    using Services.WantedAdmin;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Infrastructure.Extensions;

    public class WantedPeopleController : BaseWantedAdminController
    {
        private readonly IWantedAdminService wantedAdminService;

        public WantedPeopleController(IWantedAdminService wantedAdminService)
        {
            this.wantedAdminService = wantedAdminService;
        }

        public IActionResult Index()
        {
            // TO DO
            return View();
        }

        public IActionResult Create()
         => View(new WantedPeopleCreateFormViewModel
         {
             Languages = this.GetLanguages(),
             Countries = this.GetCountries()
         });

        [HttpPost]
        public IActionResult Create(WantedPeopleCreateFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Languages = this.GetLanguages();
                model.Countries = this.GetCountries();

                return View(model);
            }

            var existingLanguages = this.wantedAdminService.IsLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.wantedAdminService.IsCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return new BadRequestObjectResult("Unexisting language or country.");
            }

            this.wantedAdminService.Create(
                model.FirstName,
                model.LastName,
                model.Gender,
                model.DateOfBirth,
                model.PlaceOfBirth,
                model.Height,
                model.Weight,
                model.HairColor,
                model.EyeColor,
                model.PictureUrl,
                model.SelectedCountries,
                model.SelectedLanguages,
                model.AllNames,
                model.ScarsOrDistinguishingMarks);

            TempData.AddSuccessMessage("Person successfully added to the system.");

            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetLanguages()
            => this.wantedAdminService.GetLanguagesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();

        private List<SelectListItem> GetCountries()
            => this.wantedAdminService.GetCountriesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();
    }
}
