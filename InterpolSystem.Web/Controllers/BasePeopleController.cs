namespace InterpolSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.BountyAdmin;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BasePeopleController : Controller
    {
        private readonly IBountyAdminService bountyAdminService;

        protected BasePeopleController(
            IBountyAdminService bountyAdminService)
        {
            this.bountyAdminService = bountyAdminService;
        }

        protected List<SelectListItem> GetCountries()
            => this.bountyAdminService.GetCountriesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();
    }
}
