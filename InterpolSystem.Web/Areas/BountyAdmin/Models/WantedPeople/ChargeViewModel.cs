using InterpolSystem.Services.BountyAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterpolSystem.Web.Areas.BountyAdmin.Models.MissingPeople
{
    public class ChargeViewModel : LanguageAndCountryListingsViewModel
    {
        public int WantedPersonId { get; set; }

        public string Description { get; set; }


    }
}
