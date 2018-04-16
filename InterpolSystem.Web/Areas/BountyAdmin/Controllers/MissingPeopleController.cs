namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.MissingPeople;
    using Services;
    using Services.BountyAdmin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Infrastructure.Extensions;

    using static WebConstants;

    public class MissingPeopleController : BaseBountyAdminController
    {
        private readonly IBountyAdminService bountyAdminService;
        private readonly IMissingPeopleService missingPeopleService;

        public MissingPeopleController(
            IBountyAdminService bountyAdminService, 
            IMissingPeopleService missingPeopleService)
        {
            this.bountyAdminService = bountyAdminService;
            this.missingPeopleService = missingPeopleService;
        }

        public IActionResult Create()
         => View(new MissingPeopleFormViewModel
         {
             Languages = this.GetLanguages(),
             Countries = this.GetCountries()
         });

        [HttpPost]
        public IActionResult Create(MissingPeopleFormViewModel model)
        {
            var selectedLanguages = model.SelectedLanguages;
            var selectedCountries = model.SelectedCountries;

            if (selectedCountries == null || selectedLanguages == null)
            {
                ModelState.AddModelError(string.Empty, "Please choose language and nationality.");
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
                return new BadRequestObjectResult("Unexisting language or country.");
            }

            try
            {
                this.bountyAdminService.CreateMissingPerson(
                model.FirstName,
                model.LastName,
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
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            TempData.AddSuccessMessage("Person successfully added to the system.");

            return RedirectToAction(nameof(Create));
        }

        public IActionResult Edit(int id)
        {
            var existingPerson = this.missingPeopleService.IsPersonExisting(id);

            if (!existingPerson)
            {
                return new BadRequestObjectResult("Invalid person");
            }

            var person = this.missingPeopleService.GetPerson(id);

            return View(new MissingPeopleFormViewModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                AllNames = person.AllNames,
                DateOfBirth = person.DateOfBirth,
                DateOfDisappearance = person.DateOfDisappearance,
                EyeColor = person.PhysicalDescription.EyeColor,
                Gender = person.Gender,
                HairColor = person.PhysicalDescription.HairColor,
                Height = person.PhysicalDescription.Height,
                PictureUrl = person.PhysicalDescription.PictureUrl,
                Weight = person.PhysicalDescription.Weight,
                PlaceOfBirth = person.PlaceOfBirth,
                PlaceOfDisappearance = person.PlaceOfDisappearance,
                ScarsOrDistinguishingMarks = person.PhysicalDescription.ScarsOrDistinguishingMarks,
                Countries = this.GetCountries(),
                Languages = this.GetLanguages(),
                SelectedCountries = person.Nationalities.Select(n => n.Id).ToList(),
                SelectedLanguages = person.SpokenLanguages.Select(l => l.Id).ToList()
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, MissingPeopleFormViewModel model)
        {
            var exsitingPerson = this.missingPeopleService.IsPersonExisting(id);

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

            var existingLanguages = this.bountyAdminService.AreLanguagesExisting(model.SelectedLanguages);
            var existingCountries = this.bountyAdminService.AreCountriesExisting(model.SelectedCountries);

            if (!existingLanguages || !existingCountries)
            {
                return new BadRequestObjectResult("Unexisting language or country.");
            }

            try
            {
                this.bountyAdminService.EditMissingPerson(
                id,
                model.FirstName,
                model.LastName,
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
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            TempData.AddSuccessMessage($"Person {model.FirstName} {model.LastName} successfully changed");

            return RedirectToAction(nameof(Web.Controllers.MissingPeopleController.Index), MissingPeopleControllerName);
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
