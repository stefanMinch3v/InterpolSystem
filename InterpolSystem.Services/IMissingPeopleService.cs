namespace InterpolSystem.Services
{
    using Data.Models.Enums;
    using Models.MissingPeople;
    using System.Collections.Generic;

    public interface IMissingPeopleService
    {
        MissingPeopleDetailsServiceModel GetPerson(int id);

        bool IsPersonExisting(int id);

        int Total();

        int SearchPeopleCriteriaCounter { get; }

        IEnumerable<MissingPeopleListingServiceModel> All(int page = 1, int pageSize = 10);

        IEnumerable<MissingPeopleListingServiceModel> SearchByComponents(
            bool enableCountrySearch,
            int selectedCountry,
            bool enableGenderSearch,
            Gender selectedGender,
            string firstName, 
            string lastName,
            string distinguishMarks, 
            int age,
            int page = 1,
            int pageSize = 10);

        IEnumerable<LanguageListingServiceModel> GetLanguagesList();

        IEnumerable<CountryListingServiceModel> GetCountriesList();
    }
}
