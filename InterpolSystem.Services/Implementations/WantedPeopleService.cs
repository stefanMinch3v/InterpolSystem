namespace InterpolSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using InterpolSystem.Data.Models;
    using InterpolSystem.Services.Models.WantedPeople;
    using Microsoft.AspNetCore.Http;
    using Services.Models.MissingPeople;
    using System.Drawing;
    using System;

    class WantedPeopleService : IWantedPeopleService
    {
        private readonly InterpolDbContext db;

        public WantedPeopleService(InterpolDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<WantedPeopleListingServiceModel> All()
            => this.db.IdentityParticularsWanted
                    .OrderByDescending(m => m.Id)
                    .ProjectTo<WantedPeopleListingServiceModel>()
                    .ToList();

        public WantedPeopleDetailsServiceModel GetPerson(int id)
            => this.db.IdentityParticularsWanted
                .Where(m => m.Id == id)
                .ProjectTo<WantedPeopleDetailsServiceModel>()
                .FirstOrDefault();

        public bool IsPersonExisting(int id)
            => this.db.IdentityParticularsWanted.Any(m => m.Id == id);

        public void SubmitForm(int id, string policeDepartment, string subject, string message, string senderEmail,IFormFile image)
        {

            //var memoryStream = new MemoryStream();
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                //string s = Convert.ToBase64String(ms.ToArray());
                //Console.WriteLine(s);

                var form = new SubmitForm
                {
                    IdentityParticularsWantedId = id,
                    PoliceDepartment = policeDepartment,
                    Subject = subject,
                    Message = message,
                    SenderEmail = senderEmail,
                    PersonImage = ms.ToArray(),
                    
                };
            
            this.db.SubmitForms.Add(form);
            this.db.SaveChanges();
            }
        }
    }
}
