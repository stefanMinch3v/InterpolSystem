namespace InterpolSystem.Web.Areas.BountyAdmin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Area(BountyAdminArea)]
    [Authorize(Roles = WantedMissingPeopleRole + ", " + AdministratorRole)]
    public abstract class BaseBountyAdminController : Controller
    {
    }
}
