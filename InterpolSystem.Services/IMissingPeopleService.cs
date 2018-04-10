namespace InterpolSystem.Services
{
    using Models.MissingPeople;
    using System.Collections.Generic;

    public interface IMissingPeopleService
    {
        MissingPeopleDetailsServiceModel GetPerson(int id);

        IEnumerable<MissingPeopleListingServiceModel> All();

        bool IsExistingPerson(int id);
    }
}
