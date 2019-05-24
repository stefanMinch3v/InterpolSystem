namespace InterpolSystem.Services.BountyAdmin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using InterpolSystem.Services.WantedAdmin;
    using InterpolSystem.Services.WantedAdmin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WantedAdminService : IWantedAdminService
    {
        private readonly InterpolDbContext db;

        public WantedAdminService(InterpolDbContext db)
        {
            this.db = db;
        }

        public void Create(
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
            string scarsOrDistinguishingMarks = null
            )
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

            var wantedPerson = new IdentityParticularsWanted
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth,
                PlaceOfBirth = placeOfBirth,
                AllNames = allNames,
                PhysicalDescription = physicalDescription
            };
            //foreach (var countryId in countriesIds)
            //{
            //    var countryWantedAuthorities = new ChargesCountries
            //    {
            //        CountryId = countryId,

            //    };
            
            //foreach(var charge in chargesList)
            //{ 
            //    var charges = new Charges
            //    {
            //        Description = description,
            //        IdentityParticularsWantedId = wantedPerson.Id
                
            //    };

            //    var chargeCountries = new ChargesCountries
            //    {
            //        ChargesId = charges.Id,
            //        CountryId = 1
            //    };

            //    wantedPerson.Charges.Add(charges);
            //}

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
