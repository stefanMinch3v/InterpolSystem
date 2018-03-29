namespace InterpolSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserNamesMaxLength)]
        [MinLength(UserNamesMinLength)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
