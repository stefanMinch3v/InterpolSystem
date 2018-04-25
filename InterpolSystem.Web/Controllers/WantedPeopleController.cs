namespace InterpolSystem.Web.Controllers
{
    using InterpolSystem.Web.Models.Shared;
    using Microsoft.AspNetCore.Mvc;
    using Models.WantedPeople;
    using Services;
    using Services.BountyAdmin;

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
        
        public IActionResult SubmitForm(int id)
            => View(new SubmitFormViewModel
               {
                Id = id
               });

        [HttpPost]
        public IActionResult SubmitForm(SubmitFormViewModel model)
        {
            peopleService.SubmitForm(
                model.Id,
                model.PoliceDepartment,
                model.Subject,
                model.Message,
                model.Email,
                model.Image);

            return View(model);
                
        }
        
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