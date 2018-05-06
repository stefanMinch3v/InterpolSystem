namespace InterpolSystem.Api.Controllers
{
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    using static WebConstants;

    public class MissingPeopleController : BaseController
    {
        private readonly IMissingPeopleService peopleService;

        public MissingPeopleController(IMissingPeopleService peopleService)
        {
            this.peopleService = peopleService; 
        }

        [HttpGet(PageNumber)]
        [ValidateUrlId]
        public IActionResult GetList(int page)
            => Ok(this.peopleService.All(page));

        [HttpGet(PersonDetails)]
        [ValidateUrlId]
        public IActionResult GetPersonDetails(int id)
            => Ok(this.peopleService.GetPerson(id));
    }
}
