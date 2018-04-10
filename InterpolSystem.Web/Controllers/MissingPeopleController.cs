namespace InterpolSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    public class MissingPeopleController : Controller
    {
        private readonly IMissingPeopleService peopleService;

        public MissingPeopleController(IMissingPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        public IActionResult Index()
            => View(this.peopleService.All());

        public IActionResult Details(int id)
        {
            var existingPerson = this.peopleService.IsExistingPerson(id);

            if (!existingPerson)
            {
                return BadRequest();
            }

            var person = this.peopleService.GetPerson(id);

            return View(person);
        }
    }
}
