namespace InterpolSystem.Web.Areas.Wanted.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LanguageAndCountryListingsViewModel
    {
        [Display(Name = "Spoken languages")]
        public IEnumerable<SelectListItem> Languages { get; set; }

        public IEnumerable<int> SelectedLanguages { get; set; }

        [Display(Name = "Given nationalities")]
        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<int> SelectedCountries { get; set; }
    }
}
