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

        public IEnumerable<MissingPeopleListingServiceModel> All(int page = 1, int pageSize = 10)
            => this.db.IdentityParticularsMissing
                .OrderByDescending(ipm => ipm.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<MissingPeopleListingServiceModel>()
                .ToList();

        public MissingPeopleDetailsServiceModel GetPerson(int id)
            => this.db.IdentityParticularsMissing
                .Where(m => m.Id == id)
                .ProjectTo<MissingPeopleDetailsServiceModel>()
                .FirstOrDefault();

        public bool IsPersonExisting(int id)
            => this.db.IdentityParticularsMissing.Any(m => m.Id == id);

        public int Total() => this.db.IdentityParticularsMissing.Count();
    }
}
