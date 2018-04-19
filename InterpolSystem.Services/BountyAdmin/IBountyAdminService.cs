namespace InterpolSystem.Services.BountyAdmin
{
    using BountyAdmin.Models;
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IBountyAdminService
    {
        void CreateMissingPerson(
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string placeOfBirth,
            DateTime dateOfDisappearance,
            string placeOfDisappearance,
            double height,
            double weight,
            Color hairColor,
            Color eyesColor,
            string pictureUrl,
            IEnumerable<int> nationalitiesIds,
            IEnumerable<int> languagesIds,
            string allNames = null,
            string scarsOrDistinguishingMarks = null);

        void EditMissingPerson(
            int id,
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string placeOfBirth,
            DateTime dateOfDisappearance,
            string placeOfDisappearance,
            double height,
            double weight,
            Color hairColor,
            Color eyesColor,
            string pictureUrl,
            IEnumerable<int> nationalitiesIds,
            IEnumerable<int> languagesIds,
            string allNames = null,
            string scarsOrDistinguishingMarks = null);

        void CreateWantedPerson(
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
            string description,
            //IEnumerable<Charges> chargesList,
            string allNames = null,
            string scarsOrDistinguishingMarks = null);
        void EditWantedPerson(
            int id,
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

        bool AreLanguagesExisting(IEnumerable<int> ids);

        bool AreCountriesExisting(IEnumerable<int> ids);

        IEnumerable<LanguageListingServiceModel> GetLanguagesList();

        IEnumerable<CountryListingServiceModel> GetCountriesList();
        void CreateCharge(int wantedId, string description, IEnumerable<int> countriesIds);

        int GetLastPerson();
    }
}
