namespace InterpolSystem.Web.Areas.BountyAdmin.Models.WantedPeople
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ChargeViewModel
    {
        public int WantedPersonId { get; set; }

        [Required]
        [MaxLength(ChargesDescriptionMaxLength)]
        [MinLength(ChargesDescriptionMinLength)]
        [Display(Name = "Charge description")]
        public string Description { get; set; }

        [Display(Name = "Accused in countries:")]
        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<int> SelectedCountries { get; set; }
    }
}
