namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Infrastructure.Extensions;
    using InterpolSystem.Web.Areas.BountyAdmin.Models.MissingPeople;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.WantedPeople;
    using Services.BountyAdmin;
    using System.Collections.Generic;
    using System.Linq;

    public class WantedPeopleController : BaseBountyAdminController
    {
        private readonly IBountyAdminService peopleService;
        private int wantedPersonId;
        public WantedPeopleController(IBountyAdminService peopleService)
        {
            this.peopleService = peopleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCharge(int id)
        => View(new ChargeViewModel
        {
            WantedPersonId = id,
            Countries = this.GetCountries()
        });

        [HttpPost]
        public IActionResult AddCharge(ChargeViewModel model)
        {
            this.peopleService.CreateCharge(model.WantedPersonId, model.Description, model.SelectedCountries);
            model.Countries = this.GetCountries();
            model.WantedPersonId = wantedPersonId;
            TempData.AddSuccessMessage("Charge added, you can specify another one in form below.");
            return View(model);

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

            var existingLanguages = this.peopleService.AreLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.peopleService.AreCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return new BadRequestObjectResult("Unexisting language or country.");
            }

            this.peopleService.CreateWantedPerson(
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
                model.Description,
                model.ScarsOrDistinguishingMarks);

            wantedPersonId = this.peopleService.GetLastPerson();

            TempData.AddSuccessMessage("Person successfully added to the system, plase specify charges for person created");

            return RedirectToAction(nameof(AddCharge), new { id = wantedPersonId });
        }

        private List<SelectListItem> GetLanguages()
            => this.peopleService.GetLanguagesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();

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