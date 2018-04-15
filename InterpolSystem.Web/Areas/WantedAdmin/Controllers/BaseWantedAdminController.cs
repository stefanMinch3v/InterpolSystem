namespace InterpolSystem.Web.Areas.WantedAdmin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static WebConstants;

    [Area(WantedAdminArea)]
    [Authorize(Roles = WantedMissingPeopleRole + ", " + AdministratorRole)]
    public abstract class BaseWantedAdminController : Controller
    {
    }
}
