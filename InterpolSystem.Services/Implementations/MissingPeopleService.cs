namespace InterpolSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models.MissingPeople;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MissingPeopleService : IMissingPeopleService
    {
        private readonly InterpolDbContext db;

        public MissingPeopleService(InterpolDbContext db)
        {
            this.db = db;
        }

        public int SearchPeopleCriteriaCounter { get; set; }

        public MissingPeopleDetailsServiceModel GetPerson(int id)
            => this.db.IdentityParticularsMissing
                .Where(m => m.Id == id)
                .ProjectTo<MissingPeopleDetailsServiceModel>()
                .FirstOrDefault();

        public bool IsPersonExisting(int id)
            => this.db.IdentityParticularsMissing.Any(m => m.Id == id);

        public int Total() => this.db.IdentityParticularsMissing.Count();

        public IEnumerable<MissingPeopleListingServiceModel> All(int page = 1, int pageSize = 10)
            => this.db.IdentityParticularsMissing
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<MissingPeopleListingServiceModel>()
                .ToList();

        public IEnumerable<MissingPeopleListingServiceModel> SearchByComponents(
            bool enableCountrySearch,
            int selectedCountry,
            bool enableGenderSearch,
            Gender selectedGender,
            string firstName, 
            string lastName, 
            string distinguishMarks, 
            int age,
            int page = 1,
            int pageSize = 10)
        {
            var searchData = this.db.IdentityParticularsMissing
                .Include(m => m.PhysicalDescription)
                .Include(m => m.SpokenLanguages)
                .Include(m => m.Nationalities)
                .AsQueryable();

            if (enableCountrySearch && selectedCountry > 0)
            {
                searchData = searchData
                    .Where(d => d.Nationalities.Any(n => n.CountryId == selectedCountry))
                    .AsQueryable();
            }

            if (enableGenderSearch)
            {
                searchData = searchData
                    .Where(d => d.Gender == selectedGender)
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                searchData = searchData
                    .Where(d => d.FirstName.Contains(firstName))
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                searchData = searchData
                    .Where(d => d.LastName.Contains(lastName))
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(distinguishMarks))
            {
                searchData = searchData
                    .Where(d => d.PhysicalDescription.ScarsOrDistinguishingMarks.Contains(distinguishMarks))
                    .AsQueryable();
            }

            if (age > 0)
            {
                searchData = searchData
                    .Where(d => (DateTime.UtcNow.Year - d.DateOfBirth.Year) == age)
                    .AsQueryable();
            }

            this.SearchPeopleCriteriaCounter = searchData.Count();

            return searchData
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<MissingPeopleListingServiceModel>()
                .ToList();
        }
    }
}
