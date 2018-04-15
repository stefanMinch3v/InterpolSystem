namespace InterpolSystem.Services.WantedAdmin
{
    using Data.Models.Enums;
    using InterpolSystem.Services.WantedAdmin.Models;
    using System;
    using System.Collections.Generic;

    public interface IWantedAdminService
    {
        void Create(
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string placeOfBirth,
            double height,
            double weight,
            Color hairColor,
            Color eyesColor,
            string pictureUrl,
            IEnumerable<int> nationalitiesIds,
            IEnumerable<int> languagesIds,
            string allNames = null,
            string scarsOrDistinguishingMarks = null);

        bool IsLanguagesExisting(IEnumerable<int> ids);

        bool IsCountriesExisting(IEnumerable<int> ids);

        IEnumerable<LanguageListingModel> GetLanguagesList();

        IEnumerable<CountryListingModel> GetCountriesList();
    }
}
