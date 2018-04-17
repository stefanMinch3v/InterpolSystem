namespace InterpolSystem.Services
{
    using InterpolSystem.Services.Models.WantedPeople;
    using System.Collections.Generic;

    public interface IWantedPeopleService
    {
        WantedPeopleDetailsServiceModel GetPerson(int id);

        bool IsPersonExisting(int id);

        IEnumerable<WantedPeopleListingServiceModel> All();

    }
}
