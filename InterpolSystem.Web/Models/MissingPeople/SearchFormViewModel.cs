namespace InterpolSystem.Web.Models.MissingPeople
{
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchFormViewModel
    {
        public string SearchByFirstName { get; set; }

        public string SearchByLastName { get; set; }

        public string SearchByDistinguishMarks { get; set; }

        public int? SearchByAge { get; set; }
        
        [Display(Name = "Search by Gender")]
        public bool EnableGenderSearch { get; set; }

        public Gender SelectedGender { get; set; }

        [Display(Name = "Search by Country")]
        public bool EnableCountrySearch { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public int? SelectedCountryId { get; set; }
    }
}
