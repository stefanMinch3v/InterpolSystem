namespace InterpolSystem.Web.Models.Shared
{

    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class SubmitFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(PoliceDepartmentMaxLength)]
        public string PoliceDepartment { get; set; }

        [Required]
        [MaxLength(SubjectMaxLength)]
        [MinLength(SubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(MessageMaxLenght)]
        [MinLength(MessageMinLenght)]
        public string Message { get; set; }

        public IFormFile Image { get; set; }
    }
}
