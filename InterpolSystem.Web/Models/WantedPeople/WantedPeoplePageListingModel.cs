namespace InterpolSystem.Web.Models.WantedPeople
{
    using Services.Models.WantedPeople;
    using System.Collections.Generic;

    public class WantedPeoplePageListingModel 
    {
        public IEnumerable<WantedPeopleListingServiceModel> WantedPeople { get; set; } = new List<WantedPeopleListingServiceModel>();

    }
}
