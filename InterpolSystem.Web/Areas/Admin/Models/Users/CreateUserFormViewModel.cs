namespace InterpolSystem.Web.Areas.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CreateUserFormViewModel : UserRolesViewModel
    {
        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength, ErrorMessage = "Username must be at least 2 symbols long.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength, ErrorMessage = "First name must be at least 2 symbols long.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength, ErrorMessage = "Last name must be at least 2 symbols long.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Without role")]
        public bool WithoutRole { get; set; }
        
        public string Role { get; set; }
    }
}
