namespace InterpolSystem.Web.Areas.BountyHunter.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.BountyHunter;
    using System.Threading.Tasks;

    using static WebConstants;

    [Area(BountyHunterArea)]
    [Authorize(Roles = BountyHunterRole)]
    public class BountyHunterController : Controller
    {
        private readonly IBountyHunterService hunterService;
        private readonly UserManager<User> userManager;

        public BountyHunterController(
            IBountyHunterService hunterService,
            UserManager<User> userManager)
        {
            this.hunterService = hunterService;
            this.userManager = userManager;
        }

        public IActionResult GetSubmittedForms()
        {
            var currentUserId = this.userManager.GetUserId(User);

            if (currentUserId == null)
            {
                return NotFound();
            }

            var forms = this.hunterService.GetSubmittedForms(currentUserId);

            return View(forms);
        }

        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return NotFound();
            }

            var fullName = $"{currentUser.FirstName} {currentUser.LastName}";

            var certificateContent = this.hunterService.GetPdfCertificate(id, fullName, currentUser.Email);

            if (certificateContent == null)
            {
                return BadRequest();
            }

            return File(certificateContent, "application/pdf", "Official-Certificate.pdf");
        }
    }
}
