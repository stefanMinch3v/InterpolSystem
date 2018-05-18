namespace InterpolSystem.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class IdentityParticularsWanted
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(IdentityWantedNamesMaxLength)]
        [MinLength(IdentityWantedNamesMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(IdentityWantedNamesMaxLength)]
        [MinLength(IdentityWantedNamesMinLength)]
        public string LastName { get; set; }

        [MaxLength(IdentityWantedNamesMaxLength)]
        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(IdentityWantedPlaceOfBirthMaxLength)]
        [MinLength(IdentityWantedPlaceOfBirthMinLength)]
        public string PlaceOfBirth { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Reward { get; set; }

        public bool IsCaught { get; set; }

        public PhysicalDescription PhysicalDescription { get; set; }

        public List<Charges> Charges { get; set; } = new List<Charges>();

        public List<LanguagesWanted> SpokenLanguages { get; set; } = new List<LanguagesWanted>();

        public List<CountriesNationalitiesWanted> Nationalities { get; set; } = new List<CountriesNationalitiesWanted>();

        public List<SubmitForm> SubmitedForms { get; set; } = new List<SubmitForm>();
    }
}
