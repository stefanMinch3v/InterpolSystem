namespace InterpolSystem.Services.BountyAdmin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static ServiceConstants;

    public class BountyAdminService : IBountyAdminService
    {
        private readonly InterpolDbContext db;

        public BountyAdminService(InterpolDbContext db)
        {
            this.db = db;
        }

        public void CreateMissingPerson(
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
            string allNames, 
            string scarsOrDistinguishingMarks)
        {
            this.ValidateMissingPeopleData(firstName, lastName, gender, dateOfBirth, placeOfBirth, dateOfDisappearance, placeOfDisappearance, height, weight, hairColor, eyesColor, pictureUrl, nationalitiesIds, languagesIds);

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

        public void EditMissingPerson(
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
            this.ValidateMissingPeopleData(firstName, lastName, gender, dateOfBirth, placeOfBirth, dateOfDisappearance, placeOfDisappearance, height, weight, hairColor, eyesColor, pictureUrl, nationalitiesIds, languagesIds);

            if (id <= 0)
            {
                throw new InvalidOperationException(InvalidInsertedData);
            }

            var existingPerson = this.db.IdentityParticularsMissing
                .Include(p => p.PhysicalDescription)
                .Include(p => p.Nationalities)
                .Include(p => p.SpokenLanguages)
                .FirstOrDefault(p => p.Id == id);

            if (existingPerson == null)
            {
                throw new InvalidOperationException(InvalidInsertedPerson);
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

        public void CreateWantedPerson(
            string firstName, 
            string lastName,
            Gender gender, 
            DateTime dateOfBirth, 
            string placeOfBirth, 
            decimal reward,
            double height, 
            double weight, 
            Color hairColor, 
            Color eyesColor, 
            string pictureUrl,
            IEnumerable<int> nationalitiesIds, 
            IEnumerable<int> languagesIds, 
            string allNames, 
            string scarsOrDistinguishingMarks)
        {
            this.ValidateWantedPeopleData(firstName, lastName, gender, dateOfBirth, placeOfBirth, height, weight, hairColor, eyesColor, pictureUrl, nationalitiesIds, languagesIds, reward);

            var physicalDescription = new PhysicalDescription
            {
                Height = height,
                Weight = weight,
                HairColor = hairColor,
                EyeColor = eyesColor,
                PictureUrl = pictureUrl,
                ScarsOrDistinguishingMarks = scarsOrDistinguishingMarks
            };

            var wantedPerson = new IdentityParticularsWanted
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                PlaceOfBirth = placeOfBirth,
                AllNames = allNames,
                PhysicalDescription = physicalDescription,
                Reward = reward
            };

            foreach (var nationalityId in nationalitiesIds)
            {
                var countriesNationalities = new CountriesNationalitiesWanted
                {
                    CountryId = nationalityId,
                    IdentityParticularsWantedId = wantedPerson.Id
                };

                wantedPerson.Nationalities.Add(countriesNationalities);
            }

            foreach (var languageId in languagesIds)
            {
                var languageMissing = new LanguagesWanted
                {
                    LanguageId = languageId,
                    IdentityParticularsWantedId = wantedPerson.Id
                };

                wantedPerson.SpokenLanguages.Add(languageMissing);
            }

            this.db.IdentityParticularsWanted.Add(wantedPerson);
            this.db.SaveChanges();
        }

        public void EditWantedPerson(
            int id,
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string placeOfBirth,
            decimal reward,
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
            this.ValidateWantedPeopleData(firstName, lastName, gender, dateOfBirth, placeOfBirth, height, weight, hairColor, eyesColor, pictureUrl, nationalitiesIds, languagesIds, reward);

            if (id <= 0)
            {
                throw new InvalidOperationException(InvalidInsertedData);
            }

            var existingPerson = this.db.IdentityParticularsWanted
                .Include(p => p.PhysicalDescription)
                .Include(p => p.Nationalities)
                .Include(p => p.SpokenLanguages)
                .FirstOrDefault(p => p.Id == id);

            if (existingPerson == null)
            {
                throw new InvalidOperationException(InvalidInsertedPerson);
            }

            existingPerson.FirstName = firstName;
            existingPerson.LastName = lastName;
            existingPerson.Gender = gender;
            existingPerson.DateOfBirth = dateOfBirth;
            existingPerson.PlaceOfBirth = placeOfBirth;
            existingPerson.AllNames = allNames;
            existingPerson.Reward = reward;
            existingPerson.PhysicalDescription.ScarsOrDistinguishingMarks = scarsOrDistinguishingMarks;

            existingPerson.PhysicalDescription.Height = height;
            existingPerson.PhysicalDescription.Weight = weight;
            existingPerson.PhysicalDescription.HairColor = hairColor;
            existingPerson.PhysicalDescription.EyeColor = eyesColor;
            existingPerson.PhysicalDescription.PictureUrl = pictureUrl;

            // delete the current nationalities and languages and save changes in order to prevent exception
            existingPerson.Nationalities.Clear();
            existingPerson.SpokenLanguages.Clear();

            this.db.IdentityParticularsWanted.Update(existingPerson);
            this.db.SaveChanges();

            // add the new nationalities and languages in the mapping table
            AddLanguagesAndCountriesCollections(nationalitiesIds, languagesIds, existingPerson);

            this.db.IdentityParticularsWanted.Update(existingPerson);
            this.db.SaveChanges();
        }

        public void CreateCharge(int wantedId, string description, IEnumerable<int> countriesIds)
        {
            var charge = new Charges
            {
                IdentityParticularsWantedId = wantedId,
                Description = description
            };

            foreach (var countryId in countriesIds)
            {
                var countries = new ChargesCountries
                {
                    CountryId = countryId,
                    ChargesId = charge.Id
                };

                charge.CountryWantedAuthorities.Add(countries);
            }

            this.db.Charges.Add(charge);
            this.db.SaveChanges();
        }

        public bool AreCountriesExisting(IEnumerable<int> ids)
            => this.db.Countries.Any(c => !ids.Contains(c.Id));

        public bool AreLanguagesExisting(IEnumerable<int> ids)
            => this.db.Languages.Any(l => !ids.Contains(l.Id));

        public int GetLastWantedPerson()
            => this.db.IdentityParticularsWanted
                .OrderByDescending(m => m.Id)
                .FirstOrDefault().Id;

        public IEnumerable<CountryListingServiceModel> GetCountriesList()
            => this.db.Countries
                .OrderBy(c => c.Name)
                .ProjectTo<CountryListingServiceModel>()
                .ToList();

        public IEnumerable<LanguageListingServiceModel> GetLanguagesList()
            => this.db.Languages
                .OrderBy(l => l.Name)
                .ProjectTo<LanguageListingServiceModel>()
                .ToList();

        public IEnumerable<SubmitFormWantedServiceModel> GetAllSubmitForms(int commandOrder)
        {
            if (commandOrder == 0)
            {
                return this.db.SubmitForms
                        .OrderByDescending(m => m.Id)
                        .Where(m => m.Status == 0)
                        .ProjectTo<SubmitFormWantedServiceModel>()
                        .ToList();
            }

            return this.db.SubmitForms
                        .OrderByDescending(m => m.Id)
                        .ProjectTo<SubmitFormWantedServiceModel>()
                        .Where(m => m.Status != 0)
                        .ToList();
        }

        public void AcceptForm(int formId)
        {
            if (formId <= 0)
            {
                throw new InvalidOperationException(InvalidFormInfo);
            }

            var existingForm = this.db.SubmitForms.FirstOrDefault(p => p.Id == formId);
            existingForm.Status = FormOptions.Accepted;

            this.db.SubmitForms.Update(existingForm);
            this.db.SaveChanges();
        }

        public void DeclineForm(int formId)
        {
            if (formId <= 0)
            {
                throw new InvalidOperationException(InvalidFormInfo);
            }

            var existingForm = this.db.SubmitForms.FirstOrDefault(p => p.Id == formId);
            existingForm.Status = FormOptions.Declined;

            this.db.SubmitForms.Update(existingForm);
            this.db.SaveChanges();
        }

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

        private void AddLanguagesAndCountriesCollections(IEnumerable<int> nationalitiesIds, IEnumerable<int> languagesIds, IdentityParticularsWanted existingPerson)
        {
            foreach (var nationalityId in nationalitiesIds)
            {
                var countriesNationalities = new CountriesNationalitiesWanted
                {
                    CountryId = nationalityId,
                    IdentityParticularsWantedId = existingPerson.Id
                };

                existingPerson.Nationalities.Add(countriesNationalities);
            }

            foreach (var languageId in languagesIds)
            {
                var languageWanted = new LanguagesWanted
                {
                    LanguageId = languageId,
                    IdentityParticularsWantedId = existingPerson.Id
                };

                existingPerson.SpokenLanguages.Add(languageWanted);
            }
        }

        private void ValidateMissingPeopleData(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string placeOfBirth, DateTime dateOfDisappearance, string placeOfDisappearance, double height, double weight, Color hairColor, Color eyesColor, string pictureUrl, IEnumerable<int> nationalitiesIds, IEnumerable<int> languagesIds)
        {
            if (string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(placeOfBirth)
                || string.IsNullOrEmpty(placeOfDisappearance)
                || string.IsNullOrEmpty(pictureUrl)
                || dateOfBirth == null
                || dateOfDisappearance == null
                || height < 0
                || weight < 0
                || gender < 0
                || hairColor < 0
                || eyesColor < 0
                || nationalitiesIds == null
                || languagesIds == null)
            {
                throw new InvalidOperationException(InvalidInsertedData);
            }
        }

        private void ValidateWantedPeopleData(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string placeOfBirth, double height, double weight, Color hairColor, Color eyesColor, string pictureUrl, IEnumerable<int> nationalitiesIds, IEnumerable<int> languagesIds, decimal reward)
        {
            if (string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(placeOfBirth)
                || string.IsNullOrEmpty(pictureUrl)
                || dateOfBirth == null
                || height < 0
                || weight < 0
                || gender < 0
                || hairColor < 0
                || eyesColor < 0
                || nationalitiesIds == null
                || languagesIds == null
                || reward < 0)
            {
                throw new InvalidOperationException(InvalidInsertedData);
            }
        }
    }

}
