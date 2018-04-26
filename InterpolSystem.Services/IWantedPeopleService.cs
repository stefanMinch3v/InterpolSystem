namespace InterpolSystem.Services
{
    using InterpolSystem.Services.Models.WantedPeople;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public interface IWantedPeopleService
    {
        WantedPeopleDetailsServiceModel GetPerson(int id);

        bool IsPersonExisting(int id);

        IEnumerable<WantedPeopleListingServiceModel> All();

        void SubmitForm(int id, string policeDepartment, string subject, string message, string senderEmail, IFormFile image);

    }
}
