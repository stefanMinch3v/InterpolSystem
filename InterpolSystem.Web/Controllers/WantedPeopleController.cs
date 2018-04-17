namespace InterpolSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.WantedPeople;
    using Services;
    using Services.BountyAdmin;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants;

    public class WantedPeopleController : Controller
    {
        private readonly IWantedPeopleService peopleService;
        private readonly IBountyAdminService bountyAdminService;

        public WantedPeopleController(
          IWantedPeopleService peopleService,
          IBountyAdminService bountyAdminService)
        {
            this.peopleService = peopleService;
            this.bountyAdminService = bountyAdminService;
        }

        public IActionResult Index()
            => View(new WantedPeoplePageListingModel
                {
                    WantedPeople = this.peopleService.All()
                });

        private List<SelectListItem> GetCountries()
           => this.bountyAdminService.GetCountriesList()
               .Select(r => new SelectListItem
               {
                   Text = r.Name,
                   Value = r.Id.ToString()
               })
               .ToList();
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
    }
} 