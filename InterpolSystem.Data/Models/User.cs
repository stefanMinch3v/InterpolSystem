namespace InterpolSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        public List<Article> Articles { get; set; } = new List<Article>();

        // to be changed to many to many
        public List<SubmitForm> SubmitForms { get; set; } = new List<SubmitForm>();

        [NotMapped]
        public List<string> RolesIds { get; set; } = new List<string>();
    }
}
