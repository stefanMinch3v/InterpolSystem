namespace InterpolSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class LoggerService : ILoggerService
    {
        private readonly InterpolDbContext db;

        public LoggerService(InterpolDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LoggerListingServiceModel> All()
            => this.db.LogEmployees
                .OrderByDescending(l => l.Id)
                .ProjectTo<LoggerListingServiceModel>()
                .ToList();

        public int Total() => this.db.LogEmployees.Count();
    }
}
