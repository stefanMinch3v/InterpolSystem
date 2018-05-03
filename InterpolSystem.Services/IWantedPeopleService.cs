namespace InterpolSystem.Services
{
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Http;
    using Models.WantedPeople;
    using System.Collections.Generic;

    public interface IWantedPeopleService
    {
        WantedPeopleDetailsServiceModel GetPerson(int id);

        int Total();

        bool IsPersonExisting(int id);

        IEnumerable<WantedPeopleListingServiceModel> All(int page = 1, int pageSize = 10);

        void SubmitForm(
            int id, 
            string hunterId, 
            string policeDepartment, 
            string subject, 
            string message,
            string senderEmail, 
            IFormFile image);

        IEnumerable<WantedPeopleListingServiceModel> SearchByComponents(
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

        int SearchPeopleCriteriaCounter { get; }
    }
}
