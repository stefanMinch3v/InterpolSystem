namespace InterpolSystem.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using InterpolSystem.Services.Models.WantedPeople;
    using Services.Models.MissingPeople;

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



    }
}
