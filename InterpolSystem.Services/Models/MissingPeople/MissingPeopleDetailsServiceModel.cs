namespace InterpolSystem.Services.Models.MissingPeople
{
    using AutoMapper;
    using BountyAdmin.Models;
    using Common.Mapping;
    using Data.Models;
    using Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MissingPeopleDetailsServiceModel : IMapFrom<IdentityParticularsMissing>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AllNames { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public DateTime DateOfDisappearance { get; set; }

        public string PlaceOfDisappearance { get; set; }

        public PhysicalDescription PhysicalDescription { get; set; }

        public IEnumerable<LanguageListingServiceModel> SpokenLanguages { get; set; }

        public IEnumerable<CountryListingServiceModel> Nationalities { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<IdentityParticularsMissing, MissingPeopleDetailsServiceModel>()
                .ForMember(m => m.SpokenLanguages, cfg => cfg.MapFrom(s => s.SpokenLanguages.Select(l => l.Language)))
                .ForMember(m => m.Nationalities, cfg => cfg.MapFrom(s => s.Nationalities.Select(n => n.Country)));
    }
}
