using Microsoft.AspNetCore.Http;

namespace InterpolSystem.Web.Models.Shared
{
    public class SubmitFormViewModel
    {
        public int Id { get; set; }

        public string PoliceDepartment { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public IFormFile Image { get; set; }
    }
}
