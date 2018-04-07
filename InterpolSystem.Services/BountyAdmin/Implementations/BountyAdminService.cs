namespace InterpolSystem.Services.BountyAdmin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BountyAdmin.Models;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BountyAdminService : IBountyAdminService
    {
        private readonly InterpolDbContext db;

        public BountyAdminService(InterpolDbContext db)
        {
            this.db = db;
        }

        public void Create(
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
            string scarsOrDistinguishingMarks = null)
        {
            var physicalDescription = new PhysicalDescription
            {
                Height = height,
                Weight = weight,
                HairColor = hairColor,
                EyeColor = eyesColor,
                PictureUrl = pictureUrl,
                ScarsOrDistinguishingMarks = scarsOrDistinguishingMarks
            };

            var missingPerson = new IdentityParticularsMissing
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                PlaceOfBirth = placeOfBirth,
                DateOfDisappearance = dateOfDisappearance,
                PlaceOfDisappearance = placeOfDisappearance,
                AllNames = allNames,
                PhysicalDescription = physicalDescription
            };

            foreach (var nationalityId in nationalitiesIds)
            {
                var countriesNationalities = new CountriesNationalitiesMissing
                {
                    CountryId = nationalityId,
                    IdentityParticularsMissingId = missingPerson.Id
                };

                missingPerson.Nationalities.Add(countriesNationalities);
            }

            foreach (var languageId in languagesIds)
            {
                var languageMissing = new LanguagesMissing
                {
                    LanguageId = languageId,
                    IdentityParticularsMissingId = missingPerson.Id
                };

                missingPerson.SpokenLanguages.Add(languageMissing);
            }

            this.db.IdentityParticularsMissing.Add(missingPerson);
            this.db.SaveChanges();
        }

        public IEnumerable<CountryListingModel> GetCountriesList()
            => this.db.Countries
                .OrderBy(c => c.Name)
                .ProjectTo<CountryListingModel>()
                .ToList();

        public IEnumerable<LanguageListingModel> GetLanguagesList()
            => this.db.Languages
                .OrderBy(l => l.Name)
                .ProjectTo<LanguageListingModel>()
                .ToList();

        public bool IsCountriesExisting(IEnumerable<int> ids)
            => this.db.Countries.Any(c => !ids.Contains(c.Id));

        public bool IsLanguagesExisting(IEnumerable<int> ids)
            => this.db.Languages.Any(l => !ids.Contains(l.Id));
    }
}
