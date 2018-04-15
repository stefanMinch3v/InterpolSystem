namespace InterpolSystem.Services.BountyAdmin.Implementations
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
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
            if (nationalitiesIds == null || languagesIds == null)
            {
                return;
            }

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

            // add the nationalities and languages to the person
            AddLanguagesAndCountriesCollections(nationalitiesIds, languagesIds, missingPerson);

            this.db.IdentityParticularsMissing.Add(missingPerson);
            this.db.SaveChanges();
        }

        public void Edit(
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
            string scarsOrDistinguishingMarks = null)
        {
            if (nationalitiesIds == null
                || languagesIds == null
                || id <= 0)
            {
                return;
            }

            var existingPerson = this.db.IdentityParticularsMissing
                .Include(p => p.PhysicalDescription)
                .Include(p => p.Nationalities)
                .Include(p => p.SpokenLanguages)
                .FirstOrDefault(p => p.Id == id);

            if (existingPerson == null)
            {
                return;
            }

            existingPerson.FirstName = firstName;
            existingPerson.LastName = lastName;
            existingPerson.Gender = gender;
            existingPerson.DateOfBirth = dateOfBirth;
            existingPerson.PlaceOfBirth = placeOfBirth;
            existingPerson.DateOfDisappearance = dateOfDisappearance;
            existingPerson.PlaceOfDisappearance = placeOfDisappearance;
            existingPerson.AllNames = allNames;
            existingPerson.PhysicalDescription.ScarsOrDistinguishingMarks = scarsOrDistinguishingMarks;

            existingPerson.PhysicalDescription.Height = height;
            existingPerson.PhysicalDescription.Weight = weight;
            existingPerson.PhysicalDescription.HairColor = hairColor;
            existingPerson.PhysicalDescription.EyeColor = eyesColor;
            existingPerson.PhysicalDescription.PictureUrl = pictureUrl;

            // delete the current nationalities and languages and save changes in order to prevent exception
            existingPerson.Nationalities.Clear();
            existingPerson.SpokenLanguages.Clear();

            this.db.IdentityParticularsMissing.Update(existingPerson);
            this.db.SaveChanges();

            // add the new nationalities and languages in the mapping table
            AddLanguagesAndCountriesCollections(nationalitiesIds, languagesIds, existingPerson);

            this.db.IdentityParticularsMissing.Update(existingPerson);
            this.db.SaveChanges();
        }

        public bool IsCountriesExisting(IEnumerable<int> ids)
            => this.db.Countries.Any(c => !ids.Contains(c.Id));

        public bool IsLanguagesExisting(IEnumerable<int> ids)
            => this.db.Languages.Any(l => !ids.Contains(l.Id));

        private void AddLanguagesAndCountriesCollections(IEnumerable<int> nationalitiesIds, IEnumerable<int> languagesIds, IdentityParticularsMissing existingPerson)
        {
            foreach (var nationalityId in nationalitiesIds)
            {
                var countriesNationalities = new CountriesNationalitiesMissing
                {
                    CountryId = nationalityId,
                    IdentityParticularsMissingId = existingPerson.Id
                };

                existingPerson.Nationalities.Add(countriesNationalities);
            }

            foreach (var languageId in languagesIds)
            {
                var languageMissing = new LanguagesMissing
                {
                    LanguageId = languageId,
                    IdentityParticularsMissingId = existingPerson.Id
                };

                existingPerson.SpokenLanguages.Add(languageMissing);
            }
        }
    }
}
