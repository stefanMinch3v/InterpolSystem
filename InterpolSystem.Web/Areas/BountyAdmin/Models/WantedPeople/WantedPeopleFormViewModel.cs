namespace InterpolSystem.Web.Areas.BountyAdmin.Models.WantedPeople
{
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class WantedPeopleFormViewModel : LanguageAndCountryListingsViewModel, IValidatableObject
    {
        [Required]
        [MaxLength(IdentityWantedNamesMaxLength)]
        [MinLength(IdentityWantedNamesMinLength)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(IdentityWantedNamesMaxLength)]
        [MinLength(IdentityWantedNamesMinLength)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [MaxLength(IdentityWantedNamesMaxLength)]
        [Display(Name = "All names")]
        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(IdentityWantedPlaceOfBirthMaxLength)]
        [MinLength(IdentityWantedPlaceOfBirthMinLength)]
        [Display(Name = "Place of birth")]
        public string PlaceOfBirth { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Reward { get; set; }

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
