namespace InterpolSystem.Web.Controllers
{
    using InterpolSystem.Web.Infrastructure.Extensions;
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
            string ext = System.IO.Path.GetExtension(model.Image.FileName).ToLower();
            var lenght = model.Image.Length;
            if (!(ext == ".jpg" || ext == ".png" || ext == ".jpeg"))
            {
                ModelState.AddModelError(string.Empty, "File uploaded is not an image, please use .jpg/.jpeg/.png file");
            }
            if (lenght > 2000000)
            {
                ModelState.AddModelError(string.Empty, "File uploaded is too big maximum size is 2mb");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            peopleService.SubmitForm(
                model.Id,
                model.PoliceDepartment,
                model.Subject,
                model.Message,
                model.Email,
                model.Image);
            TempData.AddSuccessMessage("Form was sent to secretariat, you will be notified soon about the outcome");

            return RedirectToAction(nameof(Details), new { id = model.Id });
                
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