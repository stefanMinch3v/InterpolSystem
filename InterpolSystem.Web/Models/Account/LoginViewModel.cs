namespace InterpolSystem.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class LoginViewModel
    {
        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
