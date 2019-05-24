namespace InterpolSystem.Web.Areas.WantedAdmin.Models.WantedPeople
{
    using Data.Models.Enums;
    using InterpolSystem.Web.Areas.Wanted.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class WantedPeopleCreateFormViewModel : LanguageAndCountryListingsViewModel, IValidatableObject
    {
        private const string InvalidDateInThePast = "01/01/1800";

        [Required]
        [MaxLength(IdentityMissingNamesMaxLength)]
        [MinLength(IdentityMissingNamesMinLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(IdentityMissingNamesMaxLength)]
        [MinLength(IdentityMissingNamesMinLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [MaxLength(IdentityMissingNamesMaxLength)]
        [Display(Name = "All names")]
        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(IdentityMissingPlaceOfBirthMaxLength)]
        [MinLength(IdentityMissingPlaceOfBirthMinLength)]
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Height (in meters)")]
        public double Height { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Weight (in kg)")]
        public double Weight { get; set; }

        [Display(Name = "Color of hair")]
        public Color HairColor { get; set; }

        [Display(Name = "Color of eyes")]
        public Color EyeColor { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(PhysicalDescriptionPcitureMaxLength)]
        [MinLength(PhysicalDescriptionPcitureMinLength)]
        [RegularExpression(@"^(http|https):\/\/.*$", ErrorMessage = "The url must start with http/https://")]
        [Display(Name = "Picture URL")]
        public string PictureUrl { get; set; }

        [MaxLength(PhysicalDescriptionScarsOrMarksMaxLength)]
        [Display(Name = "Scars or any other distinguishing marks")]
        public string ScarsOrDistinguishingMarks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DateOfBirth <= DateTime.Parse(InvalidDateInThePast))
            {
                yield return new ValidationResult("Date of birth should be after 1800.");
            }

        }
    }
}
