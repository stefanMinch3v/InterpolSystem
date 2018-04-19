namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Infrastructure.Extensions;
    using InterpolSystem.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.WantedPeople;
    using Services.BountyAdmin;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants;

    public class WantedPeopleController : BaseBountyAdminController
    {
        private readonly IBountyAdminService peopleService;
        private readonly IWantedPeopleService wantedPeopleService;
        private int wantedPersonId;
        public WantedPeopleController(IBountyAdminService peopleService, IWantedPeopleService wantedPeopleService)
        {
            this.peopleService = peopleService;
            this.wantedPeopleService = wantedPeopleService;
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
            var selectedCountries = model.SelectedCountries;
            if (selectedCountries == null)
            {
                model.Countries = this.GetCountries();
                model.WantedPersonId = wantedPersonId;
                TempData.AddErrorMessage("Please choose country for particular charge");
               return View(model);
            }
           else
           {
                this.peopleService.CreateCharge(model.WantedPersonId, model.Description, model.SelectedCountries);

                model.Countries = this.GetCountries();
                model.WantedPersonId = wantedPersonId;
                TempData.AddSuccessMessage("Charge added, you can specify another one in form below.");
                return View(model);
           }
        }
        

        public IActionResult Create()
         => View(new WantedPeopleCreateFormViewModel
         {
             Languages = this.GetLanguages(),
             Countries = this.GetCountries()
         });

        public RedirectToActionResult Details()
        {
            return RedirectToAction("Details", "WantedPeople", new { area = "" });
        }
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
                model.ScarsOrDistinguishingMarks);

            wantedPersonId = this.peopleService.GetLastPerson();

            TempData.AddSuccessMessage("Person successfully added to the system, plase specify charges for person created");

            return RedirectToAction(nameof(AddCharge), new { id = wantedPersonId });
        }
        public IActionResult Edit(int id)
        {
            var existingPerson = this.wantedPeopleService.IsPersonExisting(id);

            if (!existingPerson)
            {
                return new BadRequestObjectResult("Invalid person");
            }

            var person = this.wantedPeopleService.GetPerson(id);

            return View(new WantedPeopleCreateFormViewModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                AllNames = person.AllNames,
                DateOfBirth = person.DateOfBirth,
                EyeColor = person.PhysicalDescription.EyeColor,
                Gender = person.Gender,
                HairColor = person.PhysicalDescription.HairColor,
                Height = person.PhysicalDescription.Height,
                PictureUrl = person.PhysicalDescription.PictureUrl,
                Weight = person.PhysicalDescription.Weight,
                PlaceOfBirth = person.PlaceOfBirth,
                ScarsOrDistinguishingMarks = person.PhysicalDescription.ScarsOrDistinguishingMarks,
                Countries = this.GetCountries(),
                Languages = this.GetLanguages(),
                SelectedCountries = person.Nationalities.Select(n => n.Id).ToList(),
                SelectedLanguages = person.SpokenLanguages.Select(l => l.Id).ToList()
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, WantedPeopleCreateFormViewModel model)
        {
            var exsitingPerson = this.wantedPeopleService.IsPersonExisting(id);

            if (!exsitingPerson)
            {
                return new BadRequestResult();
            }

            var selectedLanguages = model.SelectedLanguages;
            var selectedCountries = model.SelectedCountries;

            if (selectedCountries == null || selectedLanguages == null)
            {
                ModelState.AddModelError(string.Empty, "Please choose language and nationality! Refresh the page in order to see the old languages and nationalities!");
            }

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

           
            
                this.peopleService.EditWantedPerson(
                id,
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
            
           

            TempData.AddSuccessMessage($"Person {model.FirstName} {model.LastName} successfully changed");

            return RedirectToAction(nameof(Web.Controllers.WantedPeopleController.Index), WantedPeopleControllerName);
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