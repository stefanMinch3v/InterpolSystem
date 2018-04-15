namespace InterpolSystem.Services.BountyAdmin
{
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;

    public interface IBountyAdminService
    {
        void Create(
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

        void Edit(
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

        bool IsLanguagesExisting(IEnumerable<int> ids);

        bool IsCountriesExisting(IEnumerable<int> ids);
    }
}
