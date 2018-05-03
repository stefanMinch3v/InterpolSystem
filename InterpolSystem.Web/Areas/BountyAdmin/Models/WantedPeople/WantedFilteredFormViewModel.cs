namespace InterpolSystem.Web.Areas.BountyAdmin.Models.WantedPeople
{
    using Services.BountyAdmin.Models;
    using System.Collections.Generic;

    public class WantedFilteredFormViewModel
    {
        public int Type { get; set; }

        public IEnumerable<SubmitFormWantedServiceModel> Forms { get; set; }
    }
}
