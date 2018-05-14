namespace InterpolSystem.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using InterpolSystem.Services.Blog;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Shared;
    using Models.WantedPeople;
    using Services;
    using Services.BountyAdmin;
    using System;
    using System.Threading.Tasks;

    using static WebConstants;

    public class WantedPeopleController : BasePeopleController
    {
        private readonly IWantedPeopleService peopleService;
        private readonly UserManager<User> userManager;
        private readonly IArticleService articleService;

        public WantedPeopleController(
          IArticleService articleService,
          IWantedPeopleService peopleService,
          UserManager<User> userManager,
          IBountyAdminService bountyAdminService)
            : base(bountyAdminService)          
        {
            this.peopleService = peopleService;
            this.userManager = userManager;
            this.articleService = articleService;
        }

        public IActionResult Index(int page = 1)
            => View(new WantedPeoplePageListingModel
            {
                WantedPeople = this.peopleService.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.peopleService.Total() / (double)PageSize),
                Countries = this.GetCountries(),
                Articles = articleService.LastSixArticles()
                
            });

        [Authorize(Roles = BountyHunterRole)]
        public IActionResult SubmitForm(int id)
            => View(new SubmitFormViewModel { Id = id });

        [HttpPost]
        [Authorize(Roles = BountyHunterRole)]
        public async Task<IActionResult> SubmitForm(SubmitFormViewModel model)
        {
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                this.TempData.AddErrorMessage("Invalid user authentication.");
                return RedirectToAction(nameof(SubmitForm));
            }

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
                currentUser.Id,
                model.PoliceDepartment,
                model.Subject,
                model.Message,
                currentUser.Email,
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

        public IActionResult Search(SearchFormViewModel model, int page = 1)
           => View(new WantedPeoplePageListingModel
           {
               CurrentPage = page,
               WantedPeople = this.peopleService.SearchByComponents(
                   model.EnableCountrySearch,
                   model.SelectedCountryId ?? 0,
                   model.EnableGenderSearch,
                   model.SelectedGender,
                   model.SearchByFirstName,
                   model.SearchByLastName,
                   model.SearchByDistinguishMarks,
                   model.SearchByAge ?? 0,
                   page,
                   PageSize),
               SearchCriteriaTotalPages = this.peopleService.SearchPeopleCriteriaCounter,
               TotalPages = (int)Math.Ceiling(this.peopleService.SearchPeopleCriteriaCounter / (double)PageSize),
               Articles = articleService.LastSixArticles()
           });
    }
} 