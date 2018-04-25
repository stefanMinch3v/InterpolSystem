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

        [Required]
        [MaxLength(EmailMaxLength)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", 
                           ErrorMessage = "The email must be in format email@example.com")]
       
        public string Email { get; set; }

        public IFormFile Image { get; set; }
    }
}
