namespace InterpolSystem.Services
{
    using Models.MissingPeople;
    using System.Collections.Generic;

    public interface IMissingPeopleService
    {
        MissingPeopleDetailsServiceModel GetPerson(int id);

        IEnumerable<MissingPeopleListingServiceModel> All(int page = 1, int pageSize = 10);

        bool IsPersonExisting(int id);

        int Total();
    }
}
