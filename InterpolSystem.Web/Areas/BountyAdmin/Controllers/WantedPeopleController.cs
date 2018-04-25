﻿namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.WantedPeople;
    using Services;
    using Services.BountyAdmin;
    using System.Linq;
    using System.Threading.Tasks;
    using static WebConstants;

    public class WantedPeopleController : BaseBountyAdminController
    {
        private readonly IWantedPeopleService wantedPeopleService;

        public WantedPeopleController(
            IBountyAdminService bountyAdminService,
            IWantedPeopleService wantedPeopleService)
            : base(bountyAdminService)
        {
            this.wantedPeopleService = wantedPeopleService;
        }

        public IActionResult Index() => View();

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

                TempData.AddErrorMessage("Please choose country for particular charge");

                return View(model);
            }

            this.bountyAdminService.CreateCharge(model.WantedPersonId, model.Description, model.SelectedCountries);

            model.Countries = this.GetCountries();

            TempData.AddSuccessMessage("Charge added, you can specify another one in form below.");

            return View(model);
        }

        public IActionResult Create()
            => View(new WantedPeopleFormViewModel
            {
                Languages = this.GetLanguages(),
                Countries = this.GetCountries()
            });

        [HttpPost]
        public IActionResult Create(WantedPeopleFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Languages = this.GetLanguages();
                model.Countries = this.GetCountries();

                return View(model);
            }

            var existingLanguages = this.bountyAdminService.AreLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.bountyAdminService.AreCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return BadRequest("Unexisting language or country.");
            }

            this.bountyAdminService.CreateWantedPerson(
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

            var wantedPersonId = this.bountyAdminService.GetLastWantedPerson();

            TempData.AddSuccessMessage("Person successfully added to the system, plase specify charges for person created");

            return RedirectToAction(nameof(AddCharge), new { id = wantedPersonId });
        }

        public IActionResult Edit(int id)
        {
            var existingPerson = this.wantedPeopleService.IsPersonExisting(id);

            if (!existingPerson)
            {
                return BadRequest("Invalid person");
            }

            var person = this.wantedPeopleService.GetPerson(id);

            return View(new WantedPeopleFormViewModel
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
        public IActionResult Edit(int id, WantedPeopleFormViewModel model)
        {
            var exsitingPerson = this.wantedPeopleService.IsPersonExisting(id);

            if (!exsitingPerson)
            {
                return BadRequest();
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

            var existingLanguages = this.bountyAdminService.AreLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.bountyAdminService.AreCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return BadRequest("Unexisting language or country.");
            }

            this.bountyAdminService.EditWantedPerson(
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

        public IActionResult ListAllForms()
         => View(new SubmitFormWantedViewModel
         {
             SubmitForms = this.bountyAdminService.GetAllSubmitForm()
            
         });

        [HttpPost]
        public IActionResult AcceptForm(int id)
        {
            bountyAdminService.AcceptForm(id);

            return RedirectToAction(nameof(ListAllForms));
        }

        [HttpPost]
        public IActionResult DeclineForm(int id)
        {
            bountyAdminService.DeclineForm(id);

            return RedirectToAction(nameof(ListAllForms));
        }
    }
}