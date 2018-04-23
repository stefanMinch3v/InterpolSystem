namespace InterpolSystem.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class IdentityParticularsMissing
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(IdentityMissingNamesMaxLength)]
        [MinLength(IdentityMissingNamesMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(IdentityMissingNamesMaxLength)]
        [MinLength(IdentityMissingNamesMinLength)]
        public string LastName { get; set; }

        [MaxLength(IdentityMissingNamesMaxLength)]
        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(IdentityMissingPlaceOfBirthMaxLength)]
        [MinLength(IdentityMissingPlaceOfBirthMinLength)]
        public string PlaceOfBirth { get; set; }

        public DateTime DateOfDisappearance { get; set; }

        [Required]
        [MaxLength(IdentityMissingPlaceOfDisappearanceMaxLength)]
        [MinLength(IdentityMissingPlaceOfDisappearanceMinLength)]
        public string PlaceOfDisappearance { get; set; }

        public PhysicalDescription PhysicalDescription { get; set; }

        public List<LanguagesMissing> SpokenLanguages { get; set; } = new List<LanguagesMissing>();

        public List<CountriesNationalitiesMissing> Nationalities { get; set; } = new List<CountriesNationalitiesMissing>();

        public List<SubmitForm> SubmitedForms { get; set; } = new List<SubmitForm>();
    }
}
