namespace InterpolSystem.Services.Models.WantedPeople
{
    using AutoMapper;
    using BountyAdmin.Models;
    using Common.Mapping;
    using Data.Models;
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class WantedPeopleDetailsServiceModel : IMapFrom<IdentityParticularsWanted>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public decimal Reward { get; set; }

        public PhysicalDescription PhysicalDescription { get; set; }

        public IEnumerable<LanguageListingServiceModel> SpokenLanguages { get; set; }

        public IEnumerable<CountryListingServiceModel> Nationalities { get; set; }

        public IEnumerable<ChargesListingServiceModel> Charges { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<IdentityParticularsWanted, WantedPeopleDetailsServiceModel>()
                .ForMember(m => m.SpokenLanguages, cfg => cfg.MapFrom(s => s.SpokenLanguages.Select(l => l.Language)))
                .ForMember(m => m.Nationalities, cfg => cfg.MapFrom(s => s.Nationalities.Select(n => n.Country)));
    }
}
