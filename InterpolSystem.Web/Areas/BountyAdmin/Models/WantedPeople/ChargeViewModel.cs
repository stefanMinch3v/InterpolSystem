namespace InterpolSystem.Web.Areas.BountyAdmin.Models.MissingPeople
{
using InterpolSystem.Services.BountyAdmin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Data.DataConstants;

    public class ChargeViewModel : LanguageAndCountryListingsViewModel
    {
        public int WantedPersonId { get; set; }

        [Required]
        [MaxLength(ChargesDescriptionMaxLength)]
        [MinLength(ChargesDescriptionMinLength)]
        public string Description { get; set; }


    }
}
