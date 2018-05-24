namespace InterpolSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models.WantedPeople;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class WantedPeopleService : IWantedPeopleService
    {
        private readonly InterpolDbContext db;

        public WantedPeopleService(InterpolDbContext db)
        {
            this.db = db;
        }

        public int SearchPeopleCriteriaCounter { get; set; }

        public IEnumerable<WantedPeopleListingServiceModel> All(int page = 1, int pageSize = 10)
            => this.db.IdentityParticularsWanted
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<WantedPeopleListingServiceModel>()
                .ToList();

        public WantedPeopleDetailsServiceModel GetPerson(int id)
         =>     this.db.IdentityParticularsWanted
                .Where(m => m.Id == id)
                .ProjectTo<WantedPeopleDetailsServiceModel>()
                .FirstOrDefault();

        public bool IsPersonExisting(int id)
            => this.db.IdentityParticularsWanted.Any(m => m.Id == id);

        public void SubmitForm(
            int id,
            string hunterId,
            string policeDepartment, 
            string subject, 
            string message, 
            string senderEmail,
            IFormFile image)
        {
            byte[] result = null;

            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                result = ms.ToArray();
            }

            var form = new SubmitForm
            {
                IdentityParticularsWantedId = id,
                PoliceDepartment = policeDepartment,
                Subject = subject,
                Message = message,
                SenderEmail = senderEmail,
                PersonImage = result,
                SubmissionDate = DateTime.UtcNow,
                UserId = hunterId
            };

            this.db.SubmitForms.Add(form);
            this.db.SaveChanges();
        }

        public IEnumerable<WantedPeopleListingServiceModel> SearchByComponents(
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
            var searchData = this.db.IdentityParticularsWanted
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
                .ProjectTo<WantedPeopleListingServiceModel>()
                .ToList();
        }

        public int Total() => this.db.IdentityParticularsWanted.Count();
    }
}
