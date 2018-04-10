namespace InterpolSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.MissingPeople;
    using System.Collections.Generic;
    using System.Linq;

    class MissingPeopleService : IMissingPeopleService
    {
        private readonly InterpolDbContext db;

        public MissingPeopleService(InterpolDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MissingPeopleListingServiceModel> All()
            => this.db.IdentityParticularsMissing
                .OrderByDescending(ipm => ipm.Id)
                .ProjectTo<MissingPeopleListingServiceModel>()
                .ToList();

        public MissingPeopleDetailsServiceModel GetPerson(int id)
            => this.db.IdentityParticularsMissing
                .Where(m => m.Id == id)
                .ProjectTo<MissingPeopleDetailsServiceModel>()
                .FirstOrDefault();

        public bool IsExistingPerson(int id)
            => this.db.IdentityParticularsMissing.Any(m => m.Id == id);
    }
}
