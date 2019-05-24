namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.MissingPeople;
    using Services.BountyAdmin;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Infrastructure.Extensions;

    public class MissingPeopleController : BaseBountyAdminController
    {
        private readonly IBountyAdminService bountyAdminService;

        public MissingPeopleController(IBountyAdminService bountyAdminService)
        {
            this.bountyAdminService = bountyAdminService;
        }

        public IActionResult Index()
        {
            // TO DO
            return View();
        }

        public IActionResult Create()
         => View(new MissingPeopleCreateFormViewModel
         {
             Languages = this.GetLanguages(),
             Countries = this.GetCountries()
         });

        [HttpPost]
        public IActionResult Create(MissingPeopleCreateFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Languages = this.GetLanguages();
                model.Countries = this.GetCountries();

                return View(model);
            }

            var existingLanguages = this.bountyAdminService.IsLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.bountyAdminService.IsCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return new BadRequestObjectResult("Unexisting language or country.");
            }

            this.bountyAdminService.Create(
                model.FirstName,
                null,
                model.Gender,
                model.DateOfBirth,
                model.PlaceOfBirth,
                model.DateOfDisappearance,
                model.PlaceOfDisappearance,
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
            => this.bountyAdminService.GetLanguagesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();

        private List<SelectListItem> GetCountries()
            => this.bountyAdminService.GetCountriesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();
    }
}
