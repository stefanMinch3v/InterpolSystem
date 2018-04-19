namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.BountyAdmin;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants;

    [Area(BountyAdminArea)]
    [Authorize(Roles = WantedMissingPeopleRole + ", " + AdministratorRole)]
    public abstract class BaseBountyAdminController : Controller
    {
        protected readonly IBountyAdminService bountyAdminService;

        protected BaseBountyAdminController(IBountyAdminService bountyAdminService)
        {
            this.bountyAdminService = bountyAdminService;
        }

        protected List<SelectListItem> GetLanguages()
            => this.bountyAdminService.GetLanguagesList()
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString()
                })
                .ToList();

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
